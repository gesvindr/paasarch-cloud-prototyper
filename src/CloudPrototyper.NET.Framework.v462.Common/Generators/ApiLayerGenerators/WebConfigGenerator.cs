﻿using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators
{
    /// <summary>
    /// web.config.
    /// </summary>
    public class WebConfigGenerator : CodeGeneratorBase
    {
        public WebConfigGenerator(string ns, string name, GenerationInfo generationInfo) : base(ns, name, generationInfo)
        {
        }
    }
}
