using System.Collections.Generic;
using System.IO;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Framework.v462.Computing.Models;
using CloudPrototyper.NET.Framework.v462.Computing.Templates;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Computing.Generators
{
    public class ImageTresholdingGenerator : Modeled<ImageTresholding>, IOperation
    {
        public OperationInterfaceGenerator OperationInterface { get; set; }


        public override List<ContentInfo> GetContents(string projectName) => new List<ContentInfo>
        { 
            new ContentInfo
            {
                InputPath = Path.Combine(System.AppContext.BaseDirectory, "contents", "lenna.png"),
                OutputPath = Path.Combine(projectName, "contents", ModelParameters.Name, "lenna.png")
            }
         };

        public ImageTresholdingGenerator(string projectName, OperationInterfaceGenerator operationInterface, ImageTresholding modelParameters, bool canInitialize = true) : base(projectName, "Operations", modelParameters.Name, typeof(ImageTresholdingTemplate), modelParameters, modelParameters.Name, canInitialize)
        {
            OperationInterface = operationInterface;
        }
    }
}
