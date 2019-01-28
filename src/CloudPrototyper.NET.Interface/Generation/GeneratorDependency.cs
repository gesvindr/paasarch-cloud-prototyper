using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;

namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Represents depencdencies of generator enabling extensibility using .dll libraries.
    /// </summary>
    public abstract class GeneratorDependency
    { 
        /// <summary>
        /// Lists Windsor Castle IoC container registrations based of project name.
        /// </summary>
        /// <param name="projectName">Project name that the result files will be placed.</param>
        /// <returns></returns>
        public abstract List<IRegistration> GetRegistrations(string projectName);
    }

    /// <summary>
    /// Enables search dependencies for T generator.
    /// </summary>
    /// <typeparam name="T">References of generator of T type.</typeparam>
    public abstract class GeneratorDependency<T> : GeneratorDependency where T : CodeGeneratorBase
    {
        /// <summary>
        /// Type of generator.
        /// </summary>
        public Type GeneratorType => typeof(T);
    }
}
