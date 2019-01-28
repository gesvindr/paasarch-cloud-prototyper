namespace CloudPrototyper.NET.Interface.Prototyper.Prototyper
{
    public interface ILifeCycleManager
    {
        void VerifyModel();
        void PrepareResources();
        void Generate();
        void Deploy();
        void DoBenchmark();
        void CleanUp();
    }
}
