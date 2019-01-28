using System;
using System.Collections.Generic;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Resources;

namespace CloudPrototyper.Interface.Deployment
{
    /// <summary>
    /// Interface for deployment manager implementations whose 
    /// responsibility is to prepare and deploy applications/resource to Cloud.
    /// </summary>
    public interface IDeploymentManager
    {
        /// <summary>
        /// Unique name of Cloud Provider.
        /// </summary>
        string CloudProviderName { get; }

        /// <summary>
        /// Application prepared with this instance.
        /// </summary>
        List<Application> PreparedApplications { get; }

        /// <summary>
        /// Applications deployed with this instance.
        /// </summary>
        List<Application> DeployedApplications { get; }

        /// <summary>
        /// Resources prepared with this intance.
        /// </summary>
        List<Resource> PreparedResources { get; }

        /// <summary>
        /// Lists resources that can be prepared with this instance.
        /// </summary>
        /// <returns>List of resources that can be prepared with this instance.</returns>
        List<Type> GetSupportedResources();

        /// <summary>
        /// Lists applications that can be prepared with this instance.
        /// </summary>
        /// <returns>List of applications that can be prepared with this instance.</returns>
        List<Type> GetSupportedApplications();

        /// <summary>
        /// ConfigurationProvider instatnce provides elementary
        /// info like user name or passwork to communicate with Cloud provider.
        /// </summary>
        IConfigProvider ConfigProvider { get; set; }

        /// <summary>
        /// Injects IConfigProvider instance.
        /// </summary>
        /// <param name="configProvider">IConfigProvider instance.</param>
        void Setup(IConfigProvider configProvider);

        /// <summary>
        /// Prepares set of applications and store them into PreparedApplications.
        /// </summary>
        /// <param name="applications">Applications to be prepared.</param>
        void PrepareApplications(List<Application> applications);

        /// <summary>
        /// Deploys set of applications and store them into DeployedApplications.
        /// </summary>
        /// <param name="applications">Applications to be deployed.</param>
        void DeployApplications(List<Application> applications); 

        /// <summary>
        /// Prepares set of applications and store them into PreparedApplications.
        /// </summary>  
        /// <param name="resources">Applications to be prepared.</param>
        void PrepareResources(List<Resource> resources);

        /// <summary>
        /// Deallocates all resouces and applications.
        /// </summary>
        void Clear();
    }
}
