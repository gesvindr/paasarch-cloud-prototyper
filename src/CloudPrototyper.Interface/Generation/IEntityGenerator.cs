using System.Collections.Generic;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Interface.Prototyper;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Resources.Storage;

namespace CloudPrototyper.Interface.Generation
{
    /// <summary>
    /// Interface for data generators.
    /// </summary>
    public interface IEntityGenerator
    {
        /// <summary>
        /// Setups properties of the generator.
        /// </summary>
        /// <param name="prototype">Prototype.</param>
        /// <param name="generatorManager">Generator manager of data layer.</param>
        /// <param name="configProvider">Config provider.</param>
        /// <returns></returns>
        List<EntityStorage> Setup(Prototype prototype, GeneratorManager generatorManager, IConfigProvider configProvider);


        /// <summary>
        /// Returns content based on decription.
        /// </summary>
        void Generate();
    }
}
