using System;
using System.IO;
using CloudPrototyper.Examples;
using CloudPrototyper.Model;
using Newtonsoft.Json;

namespace CloudPrototyper.Console
{
    /// <summary>
    /// Entry point of application. Triggers all steps of prototype of life cycle.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of application. Triggers all steps of prototype of life cycle.
        /// Loads JSON, creates ModelResolver instatnce and calls its methods.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            string json = string.Empty;
            System.Console.WriteLine("Type path to file with model in JSON format:");
            string modelJsonFilePath = System.Console.ReadLine();
            System.Console.WriteLine("Type path to config file:");
            string configFilePath = System.Console.ReadLine();


            if (!string.IsNullOrEmpty(modelJsonFilePath))
            {
                json = File.ReadAllText(modelJsonFilePath);
            }
            else
            {
                modelJsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SocialNetworkModel.json");
                json = File.ReadAllText(modelJsonFilePath);
                System.Console.WriteLine("Social Network sample is used.");
            }

            if (string.IsNullOrEmpty(configFilePath))
            {
                configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"prototyper.config");
                System.Console.WriteLine("Default config file is used.");
            }


            System.Diagnostics.Trace.WriteLine(json);
            Prototype prototype = JsonConvert.DeserializeObject<Prototype>(json, settings);

            TapHomeSample tapHomeSample = new TapHomeSample();
            var prototypeManager = new ModelResolver.Implementations.ModelResolver(tapHomeSample.SqlVersionSynchronousModel, configFilePath);
          /*  System.Console.WriteLine("Verifying model");
            prototypeManager.VerifyModel();
            System.Console.WriteLine("Model is OK");
            */
          //  var prototypeManager = new ModelResolver.Implementations.ModelResolver(tapHomeSample.NoSqlVersionSynchronousModel, configFilePath);

         /*   System.Console.WriteLine("Verifying model");
            prototypeManager.VerifyModel();
            System.Console.WriteLine("Model is OK");
            */
          //  var prototypeManager = new ModelResolver.Implementations.ModelResolver(tapHomeSample.NoSqlVersionAsynchronousModel, configFilePath);
            
            System.Console.WriteLine("Verifying model");
            prototypeManager.VerifyModel();
            System.Console.WriteLine("Model is OK");
            
            System.Console.WriteLine("Preparing resources");
            prototypeManager.PrepareResources();
            System.Console.WriteLine("Prepared"); 

            System.Console.WriteLine("Generating prototype");
            prototypeManager.Generate();
            System.Console.WriteLine("Generated");
            
            System.Console.WriteLine("Deploying");
            prototypeManager.Deploy();
            System.Console.WriteLine("Deployed");
            
            System.Console.WriteLine("Generating benchmark");
            prototypeManager.GenerateBenchmark();
            System.Console.WriteLine("Done");

            //Automated cleanup is not required. User has to delete resource group and output files on his own.
            /* System.Console.WriteLine("Cleaning");
             prototypeManager.CleanUp();
             System.Console.WriteLine("Cleaned");*/


            System.Console.ReadKey();
        }
    }
}
