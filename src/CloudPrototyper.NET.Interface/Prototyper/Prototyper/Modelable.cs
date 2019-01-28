using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;

namespace CloudPrototyper.Interface.Prototyper
{
    /// <summary>
    /// Every "generator"/"template model" should inherit this class where T is abstract model 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Modelable<T> : GenerableBase where T : class 
    {
        public T ModelParameters { get; set; }
        protected Modelable(GenerationInfo generationInfo, T modelParameters) : base(generationInfo)
        {
            ModelParameters = modelParameters;
        }
    }
}
