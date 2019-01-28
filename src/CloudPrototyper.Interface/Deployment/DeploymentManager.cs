using System;
using System.Collections.Generic;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Resources;

namespace CloudPrototyper.Interface.Deployment
{
    /// <summary>
    /// Abstract base class for deployment manager implementations whose 
    /// responsibility is to prepare and deploy applications/resource to Cloud.
    /// </summary>
    public abstract class DeploymentManager : IDeploymentManager
    {
        /// <summary>
        /// Unique name of Cloud Provider.
        /// </summary>
        public string CloudProviderName { get; }

        /// <summary>
        /// Application prepared with this instance.
        /// </summary>
        public List<Application> PreparedApplications { get; } = new List<Application>();

        /// <summary>
        /// Applications deployed with this instance.
        /// </summary>
        public List<Application> DeployedApplications { get; } = new List<Application>();

        /// <summary>
        /// Resources prepared with this intance.
        /// </summary>
        public List<Resource> PreparedResources { get; } = new List<Resource>();

        /// <summary>
        /// Lists resources that can be prepared with this instance.
        /// </summary>
        /// <returns>List of resources that can be prepared with this instance.</returns>
        public abstract List<Type> GetSupportedResources();

        /// <summary>
        /// Lists applications that can be prepared with this instance.
        /// </summary>
        /// <returns>List of applications that can be prepared with this instance.</returns>
        public abstract List<Type> GetSupportedApplications();

        /// <summary>
        /// ConfigurationProvider instatnce provides elementary
        /// info like user name or passwork to communicate with Cloud provider.
        /// </summary>
        public IConfigProvider ConfigProvider { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected DeploymentManager()
        {
            
        }

        /// <summary>
        /// Construstor.
        /// </summary>
        /// <param name="cloudProvideraName">Cloud providers name.</param>
        protected DeploymentManager(string cloudProvideraName)
        {
            CloudProviderName = cloudProvideraName;
        }

        /// <summary>
        /// Injects IConfigProvider instance.
        /// </summary>
        /// <param name="configProvider">IConfigProvider instance.</param>
        public virtual void Setup(IConfigProvider configProvider)
        {
            ConfigProvider = configProvider;
        }

        /// <summary>
        /// Prepares set of applications and store them into PreparedApplications.
        /// </summary>
        /// <param name="applications">Applications to be prepared.</param>
        public abstract void PrepareApplications(List<Application> applications);

        /// <summary>
        /// Deploys set of applications and store them into DeployedApplications.
        /// </summary>
        /// <param name="applications">Applications to be deployed.</param>
        public abstract void DeployApplications(List<Application> applications);

        /// <summary>
        /// Prepares set of applications and store them into PreparedApplications.
        /// </summary>  
        /// <param name="resources">Applications to be prepared.</param>
        public abstract void PrepareResources(List<Resource> resources);

        /// <summary>
        /// Deallocates all resouces and applications.
        /// </summary>
        public abstract void Clear();

    }
}
