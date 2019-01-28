using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using CloudPrototyper.Interface.Build;
using CloudPrototyper.Interface.Constants;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;

namespace CloudPrototyper.Build.NET
{
    /// <summary>
    /// Implementation of IBuilder for DotNet46 platform.
    /// </summary>
    public class DotNetBuilder : BuilderBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public DotNetBuilder() : base(new List<string> { "DotNet46"})
        {
        }

        /// <summary>
        /// DotNetBuilder builds .NET solution using nuget.exe to restore NuGets and
        /// Microsoft.Build library to build.
        /// </summary>
        /// <param name="configProvider">Provides configuration</param>
        /// <param name="buildable">Represents buildable .NET solution</param>
        public override void Build(IConfigProvider configProvider, IBuildable buildable)
        {
            RestoreNugets(configProvider,buildable);
            MakeBuild(configProvider, buildable);
        }

        /// <summary>
        /// Set up parametrs of build and make build, NuGets must be RESTORED!
        /// </summary>
        /// <param name="configProvider">Provides configuration</param>
        /// <param name="buildable">Represents buildable .NET solution</param>
        private void MakeBuild(IConfigProvider configProvider, IBuildable buildable)
        {
            try
            {
                ProjectCollection pc = new ProjectCollection();

                Dictionary<string, string> globalProperty = new Dictionary<string, string>
                {
                    {"Configuration", "Release"},
                    {"Platform", "Any CPU"},
                    {"OutputPath", buildable.OutputBuildPath+"\\build"}
                };

                BuildParameters bp = new BuildParameters(pc);
                File.WriteAllText(buildable.OutputBuildPath + "\\build.log", "");
                bp.Loggers = new[] {
                    new FileLogger
                    {
                        Verbosity = LoggerVerbosity.Detailed,
                        ShowSummary = true,
                        SkipProjectStartedText = false,
                        Parameters = buildable.OutputBuildPath + "\\build.log"
            }
                };
                BuildRequestData buidlRequest = new BuildRequestData(Directory.GetFiles(buildable.SolutionRootPath, "*.sln", SearchOption.AllDirectories).First(), globalProperty, "4.0", new[] { "Build" }, null);
                BuildResult buildResult = BuildManager.DefaultBuildManager.Build(bp, buidlRequest);

                if (buildResult.OverallResult != BuildResultCode.Success)
                { 
                   throw new ArgumentException("Provided buildable is not valid :( " + buildable.SolutionRootPath); 
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Provided buildable is not valid.", e);
            }
        }

        /// <summary>
        /// Opens nuget.exe with solution file as a parameter and
        /// </summary>
        /// <param name="configProvider">Provides configuration</param>
        /// <param name="buildable">Represents buildable .NET solution</param>
        private void RestoreNugets(IConfigProvider configProvider, IBuildable buildable)
        {

            try
            {
                var isDone = false;
                var solutionFile = Directory.GetFiles(buildable.SolutionRootPath, "*.sln", SearchOption.AllDirectories).First();
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "nuget.exe"),
                    Arguments = "restore " + solutionFile,
                    UseShellExecute = false
                    
                };

                process.EnableRaisingEvents = true;
                process.StartInfo = startInfo;

                process.OutputDataReceived += (sender, args) =>
                {
                    Console.WriteLine(args.ToString());
                };
                process.Exited += (sender, args) =>
                {
                    isDone = true;
                    process.Dispose();
                };
                process.Start();

                while (!isDone)
                {
                    Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Provided buildable is not valid:  " + e.Message);
            }
        }
    }
}
