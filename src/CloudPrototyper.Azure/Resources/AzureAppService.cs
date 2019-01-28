using CloudPrototyper.Model.Resources;

namespace CloudPrototyper.Azure.Resources
{
    /// <summary>
    /// Azure App Service Representation.
    /// </summary>
    public class AzureAppService : HostingEnvironment
    {
        /// <summary>
        /// Performance tier.
        /// </summary>
        public string PerformanceTier { get; set; }

        /// <summary>
        /// Plan name.
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// Application to be deployed here.
        /// </summary>
        public string WithApplication { get; set; }
    }
}
