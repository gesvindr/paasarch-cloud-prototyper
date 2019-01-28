namespace CloudPrototyper.Interface.Prototyper
{
    /// <summary>
    /// Implementation of this interface manages whole prototype life cycle from verifying model
    /// to cleaning up.
    /// </summary>
    public interface ILifeCycleManager
    {
        /// <summary>
        /// Generates and build prototype in order to verify model.
        /// </summary>
        void VerifyModel();

        /// <summary>
        /// Prepares resources in order to be prepared.
        /// </summary>
        void PrepareResources();

        /// <summary>
        /// Generates and build prototype with resources informations within.
        /// </summary>
        void Generate();

        /// <summary>
        /// Uploads applications into cloud environment.
        /// </summary>
        void Deploy();

        /// <summary>
        /// Generates benchmark file output based on IBenchmarkGenerator implementation.
        /// </summary>
        void GenerateBenchmark();

        /// <summary>
        /// Deallocate from cloud, deletes generated source files.
        /// </summary>
        void CleanUp();
    }
}
