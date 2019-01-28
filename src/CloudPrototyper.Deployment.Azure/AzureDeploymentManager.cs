using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using CloudPrototyper.Azure.Resources;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Interface.Deployment;
using CloudPrototyper.Model.Applications;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;
using Resource = CloudPrototyper.Model.Resources.Resource;
using CloudPrototyper.NET.Framework.v462.TblStorage.Model;
using FluentFTP;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.Management.Sql.Fluent.Models;
using Microsoft.Azure.Management.Storage.Fluent;

namespace CloudPrototyper.Deployment.Azure
{
    /// <summary>
    /// The implementation of DeploymentManager for Azure Cloud. 
    /// Before usage it is needed to have Service App - Owner with config keys:
    /// "ClientId"
    /// "TennantId" 
    /// "ClientSecret" 
    /// "SubscriptionId"
    /// 
    /// The subscription need several registrations via PowerShell:
    /// Login-AzureRmAccount
    /// Register-AzureRmResourceProvider 
    /// a) -ProviderNamespace Microsoft.DataFactory
    /// b) -ProviderNamespace Microsoft.Cache
    /// c) -ProviderNamespace Microsoft.Storage
    /// d) -ProviderNamespace Microsoft.ServiceBus
    /// </summary>
    public class AzureDeploymentManager : DeploymentManager
    {
        private IAzure _azure;
        private IResourceGroup _resourceGroup;
        private ISqlServer _sqlServer;
        private readonly Dictionary<Application, IWebApp> _webApps = new Dictionary<Application, IWebApp>();
        private readonly Dictionary<AzureSQLDatabase, ISqlDatabase> _databases = new Dictionary<AzureSQLDatabase, ISqlDatabase>();
        private readonly Dictionary<AzureServiceBusQueue, IQueue> _serviceQueues = new Dictionary<AzureServiceBusQueue, IQueue>();
        private readonly Dictionary<AzureTableStorage, IStorageAccount> _tableStorages = new Dictionary<AzureTableStorage, IStorageAccount>();
        private readonly Dictionary<Application, AzureAppService> _appServices = new Dictionary<Application, AzureAppService>();
        private readonly Dictionary<AzureAppService, IWebApp> _webAppList = new Dictionary<AzureAppService, IWebApp>();
        private bool _initialized;

        /// <summary>
        /// Constructor
        /// </summary>
        public AzureDeploymentManager() : base("Azure")
        {

        }

        /// <summary>
        /// Lists resources that can be prepared with this instance.
        /// </summary>
        /// <returns>List of resources that can be prepared with this instance.</returns>
        public override List<Type> GetSupportedResources()
            =>
                new List<Type>
                {
                    typeof (AzureSQLDatabase),
                    typeof (AzureTableStorage),
                    typeof (AzureServiceBusQueue),
                    typeof (AzureAppService)
                };

        /// <summary>
        /// Lists applications that can be prepared with this instance.
        /// </summary>
        /// <returns>List of applications that can be prepared with this instance.</returns>
        public override List<Type> GetSupportedApplications() =>
                new List<Type>
                {
                    typeof (RestApiApplication),
                    typeof (WorkerApplication)
                };

        /// <summary>
        /// Prepares set of applications and store them into PreparedApplications.
        /// </summary>
        /// <param name="applications">Applications to be prepared.</param>
        public override void PrepareApplications(List<Application> applications)
        {
            foreach (var app in applications)
            {
                _appServices.Add(app, _webAppList.Single(x => x.Key.WithApplication == app.Name).Key);
                _webApps.Add(app,_webAppList.Single(x => x.Key.WithApplication == app.Name).Value);
                PreparedApplications.Add(app);
            }
        }

        /// <summary>
        /// Deploys set of applications and store them into DeployedApplications.
        /// </summary>
        /// <param name="applications">Applications to be deployed.</param>
        public override void DeployApplications(List<Application> applications)
        {
            Init();

            foreach (var application in applications)
            {
                if (PreparedApplications.Contains(application))
                {
                    Deploy((dynamic)application);
                }
            }
        }

        /// <summary>
        /// Deallocates all resouces and applications.
        /// </summary>
        public override void Clear()
        {
            Console.WriteLine("Deleting resource group");
            _azure.ResourceGroups.DeleteByName(_resourceGroup.Name);
        }

        /// <summary>
        /// Prepares set of applications and store them into PreparedApplications.
        /// </summary>  
        /// <param name="resources">Applications to be prepared.</param>
        public override void PrepareResources(List<Resource> resources)
        {
            Init();
            foreach (var resource in resources)
            {
                PrepareResource((dynamic)resource);
            }
        }
        private void Deploy(Application application)
        {
            // Not supported
        }

        private void Deploy(WorkerApplication application)
        {
            var publishProfile = _webApps[application].GetPublishingProfile();

            

            FtpClient client = new FtpClient();
            var url = publishProfile.FtpUrl;

            Uri myUri = new Uri("https://" + url);
            var ip = Dns.GetHostAddresses(myUri.Host)[0];
            client.Host = ip.ToString();

            client.Credentials = new NetworkCredential(publishProfile.FtpUsername, publishProfile.FtpPassword);
            var files =
                Directory.GetFiles(
                    Path.Combine(ConfigProvider.GetValue("OutputFolderPath"), application.Name, "build"), "*",
                    SearchOption.AllDirectories);
            foreach (var file in files)
            {
                Uri fullPath = new Uri(file, UriKind.Absolute);
                Uri relRoot = new Uri(Path.Combine(ConfigProvider.GetValue("OutputFolderPath"), application.Name, "build"), UriKind.Absolute);

                string relPath = relRoot.MakeRelativeUri(fullPath).ToString();
                client.UploadFile(file, (@"\site\wwwroot\App_Data\Jobs\continuous\Worker" + "\\" + relPath.Substring(6)), true, true);
                if (Path.GetFileName(file.ToLower()).Contains("packages.config"))
                {
                    client.UploadFile(file, (@"\site\wwwroot" + "\\" + relPath.Substring(6)), true, true);
                }
            }
            _webApps[application].Refresh();
            DeployedApplications.Add(application);
            client.Dispose();
        }

        private void Deploy(RestApiApplication application)
        {

            var publishProfile = _webApps[application].GetPublishingProfile();
            FtpClient client = new FtpClient();
            var url = publishProfile.FtpUrl;

            Uri myUri = new Uri("https://" + url);
            var ip = Dns.GetHostAddresses(myUri.Host)[0];
            client.Host = ip.ToString();

            client.Credentials = new NetworkCredential(publishProfile.FtpUsername, publishProfile.FtpPassword);
            var files =
                Directory.GetFiles(
                    Path.Combine(ConfigProvider.GetValue("OutputFolderPath"), application.Name, "build"), "*",
                    SearchOption.AllDirectories);
            foreach (var file in files)
            {
                Uri fullPath = new Uri(file, UriKind.Absolute);
                Uri relRoot = new Uri(Path.Combine(ConfigProvider.GetValue("OutputFolderPath"), application.Name, "build"), UriKind.Absolute);

                string relPath = relRoot.MakeRelativeUri(fullPath).ToString();
                if (Path.GetFileName(file.ToLower()).Contains("global.asax") ||
                    Path.GetFileName(file.ToLower()).Contains("web.config") ||
                    Path.GetFileName(file.ToLower()).Contains("packages.config"))
                {
                    client.UploadFile(file, (@"\site\wwwroot" + "\\" + relPath.Substring(6)), true, true);
                }
                else
                {
                    client.UploadFile(file, (@"\site\wwwroot\bin" + "\\" + relPath.Substring(6)), true, true);
                }
            }
            application.BaseUrl = _webApps[application].DefaultHostName;
            DeployedApplications.Add(application);
            client.Dispose();
        }

        private void PrepareResource(Resource resource)
        {
            // Not supported
        }

        private void PrepareResource(AzureAppService resource)
        {
            Init();
            PricingTier tier = PricingTier.FreeF1;
            switch (resource.PerformanceTier)
            {
                case "FreeF1":
                    tier = PricingTier.FreeF1;
                    break;
                case "BasicB1":
                    tier = PricingTier.BasicB1;
                    break;
                case "BasicB2":
                    tier = PricingTier.BasicB2;
                    break;
                case "BasicB3":
                    tier = PricingTier.BasicB3;
                    break;
                case "PremiumP1":
                    tier = PricingTier.PremiumP1;
                    break;
                case "PremiumP2":
                    tier = PricingTier.PremiumP2;
                    break;
                case "PremiumP3":
                    tier = PricingTier.PremiumP3;
                    break;
                case "SharedD1":
                    tier = PricingTier.SharedD1;
                    break;
                case "StandardS1":
                    tier = PricingTier.StandardS1;
                    break;
                case "StandardS2":
                    tier = PricingTier.StandardS2;
                    break;
                case "StandardS3":
                    tier = PricingTier.StandardS3;
                    break;
            }
            Console.WriteLine("Making app service: " + resource.Name);
            var app = _azure.WebApps
                .Define(Guid.NewGuid().ToString("N").Substring(0, 8))
                .WithRegion(ConfigProvider.GetValue("AzureRegion"))
                .WithExistingResourceGroup(_resourceGroup.Name)
                .WithNewWindowsPlan(tier)
                .Create();
            PreparedResources.Add(resource);
            Console.WriteLine("Done: " + resource.Name);


            _webAppList.Add(resource, app);
        }
        private void PrepareResource(AzureSQLDatabase resource)
        {
            var serverLogin = "sqladmin3423";
            var serverPass = "myS3cureP@ssword";
            if (_sqlServer == null)
            {
                Console.WriteLine("Making sql server");
                _sqlServer = _azure.SqlServers.Define(Guid.NewGuid().ToString("N").Substring(0, 16))
                        .WithRegion(ConfigProvider.GetValue("AzureRegion"))
                        .WithExistingResourceGroup(_resourceGroup)
                        .WithAdministratorLogin("sqladmin3423")
                        .WithAdministratorPassword("myS3cureP@ssword")
                        .WithNewFirewallRule("0.0.0.0", "255.255.255.255", "shady")
                        .Create();
            }
            Console.WriteLine("Making sql db: " + resource.Name);
            _databases.Add(resource, _sqlServer.Databases.Define(resource.Name)
                .WithEdition(resource.PerformanceTier).WithServiceObjective(resource.ServiceObjective)
                        .Create());

            PreparedResources.Add(resource);
            resource.ConnectionString = "Server=tcp:" + _sqlServer.Name + ".database.windows.net,1433;Initial Catalog=" +
                                        _databases[resource].Name + ";Persist Security Info=False;User ID=" +
                                        serverLogin + ";Password=" + serverPass +
                                        ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            Console.WriteLine("Done: " + resource.Name);
        }
        private void PrepareResource(AzureServiceBusQueue resource)
        {

            Console.WriteLine("Making AzureServiceBusQueue: " + resource.Name);
            var queueName = resource.Name;
            var serviceBusNamespace = _azure.ServiceBusNamespaces
                .Define(SdkContext.RandomResourceName("namespace",20))
                .WithRegion(_resourceGroup.RegionName)
                .WithExistingResourceGroup(_resourceGroup)
                .WithNewQueue(queueName, resource.SizeInGB*1024)
                .Create();

            var firstQueue = serviceBusNamespace.Queues.GetByName(queueName);
            var rule = firstQueue.AuthorizationRules.Define("rulerule").WithManagementEnabled().Create();
            
            resource.ConnectionString = rule.GetKeys().PrimaryConnectionString;
            
            Console.WriteLine("Making AzureServiceBusQueue: " + resource.Name);

            PreparedResources.Add(resource);
            _serviceQueues.Add(resource, firstQueue);
                                

            Console.WriteLine("Done: " + resource.Name);
        }
        private void PrepareResource(AzureTableStorage resource)
        {
            Console.WriteLine("Making AzureTableStorage: " + resource.Name);
            var storageAccount = _azure.StorageAccounts.Define(Guid.NewGuid().ToString("N").Substring(0, 16))
                    .WithRegion(_resourceGroup.RegionName)
                    .WithExistingResourceGroup(_resourceGroup)
                    .Create();

            PreparedResources.Add(resource);
            _tableStorages.Add(resource, storageAccount);
            resource.ConnectionString = "DefaultEndpointsProtocol=https;AccountName=" + storageAccount.Name +
                                        ";AccountKey=" +
                                        storageAccount.GetKeys().First().Value +
                                        ";EndpointSuffix=core.windows.net";

            Console.WriteLine("Done: " + resource.Name);
        }

        private void Init()
        {
            if (!_initialized)
            {
                Console.WriteLine("Azure authentization");
                var azureRegion = ConfigProvider.TryGetValue("AzureRegion");
                _azure =
                    Microsoft.Azure.Management.Fluent.Azure.Authenticate(new AzureCredentials(
                        new ServicePrincipalLoginInformation { ClientId = ConfigProvider.GetValue("ClientId"), ClientSecret = ConfigProvider.GetValue("ClientSecret") }, ConfigProvider.GetValue("TennantId"),
                        AzureEnvironment.AzureGlobalCloud)).WithSubscription(ConfigProvider.GetValue("SubscriptionId"));
                Console.WriteLine("Making resource group");
                _resourceGroup = _azure.ResourceGroups
                        .Define(ConfigProvider.TryGetValue("ResourceGroupName") ?? "CloudPrototyperGroup")
                        .WithRegion(azureRegion ?? "westeurope")
                        .Create();

                _initialized = true;
            }
        }

    }
}
