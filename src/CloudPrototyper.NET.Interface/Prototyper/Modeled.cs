using System;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Interface.Prototyper
{
    /// <summary>
    /// Generators that implements support for a part of prototype model.
    /// </summary>
    public abstract class Modelable : CodeGeneratorBase, IUnambiguous
    {
        /// <summary>
        /// Unique identifier of model part.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="projectName">Project name.</param>
        /// <param name="relativeFolder">Relative folder in result solution.</param>
        /// <param name="name">Name of file.</param>
        /// <param name="templateType">File template.</param>
        /// <param name="key">Model part identification.</param>
        /// <param name="canInitialize">Determines if the template can be initialized.</param>
        protected Modelable(string projectName, string relativeFolder, string name, Type templateType, string key, bool canInitialize = true) : base(projectName, relativeFolder, name, templateType, canInitialize)
        {
            Key = key;
        }
    }
    /// <summary>
    /// Every "generator"/"template model" should inherit this class where T is a part the prototype model. 
    /// </summary>
    /// <typeparam name="T">Part of model like operation or entity storage.</typeparam>
    public abstract class Modeled<T> : Modelable where T : class
    {
        /// <summary>
        /// Part of model like operation or entity storage.
        /// </summary>
        public T ModelParameters { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="projectName">Project name.</param>
        /// <param name="relativeFolder">Relative folder in result solution.</param>
        /// <param name="name">Name of file.</param>
        /// <param name="templateType">File template.</param>
        /// <param name="modelParameters">Part of model like operation or entity storage.</param>
        /// <param name="key">Model part identification.</param>
        /// <param name="canInitialize">Determines if the template can be initialized.</param>
        protected Modeled(string projectName, string relativeFolder, string name, Type templateType, T modelParameters, string key,
            bool canInitialize = true) : base(projectName, relativeFolder, name, templateType, key, canInitialize)
        {
            ModelParameters = modelParameters;
        }
    }
}
