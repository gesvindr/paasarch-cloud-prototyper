﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CloudPrototyper.NET.Framework.v462.Common.Templates.SolutionTemplates.Assemblies
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.IO;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class LibraryAssemblyTemplate : LibraryAssemblyTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""14.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <Import Project=""$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"" Condition=""Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')"" />
  <PropertyGroup>
    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
    <Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>
    <ProjectGuid>{");
            
            #line 14 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.AssemblyInfo.UniqueProjectId.ToString().ToUpper()));
            
            #line default
            #line hidden
            this.Write("}</ProjectGuid>\r\n    <OutputType>Library</OutputType>\r\n    <AppDesignerFolder>Pro" +
                    "perties</AppDesignerFolder>\r\n    <RootNamespace>");
            
            #line 17 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.AssemblyInfo.Name));
            
            #line default
            #line hidden
            this.Write("</RootNamespace>\r\n    <AssemblyName>");
            
            #line 18 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.AssemblyInfo.Name));
            
            #line default
            #line hidden
            this.Write(@"</AssemblyName>
    <TargetFrameworkVersion>v4.6.2</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' "">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include=""System""/>
    <Reference Include=""System.Core""/>
    <Reference Include=""System.Drawing"" />
    <Reference Include=""System.Xml.Linq""/>
	<Reference Include=""System.Runtime.Serialization"" />
    <Reference Include=""System.ComponentModel.DataAnnotations"" />
    <Reference Include=""System.Data.DataSetExtensions""/>
    <Reference Include=""Microsoft.CSharp""/>
    <Reference Include=""System.Data""/>
    <Reference Include=""System.Net.Http""/>
    <Reference Include=""System.Xml""/>  
	</ItemGroup>
<ItemGroup>
");
            
            #line 53 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 foreach(var reference in Model.AssemblyInfo.Packages) {
            
            #line default
            #line hidden
            
            #line 54 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 foreach(var package in reference.IncludeHintPathTuples) {
            
            #line default
            #line hidden
            this.Write("\t<Reference Include=\"");
            
            #line 55 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(package.Item1));
            
            #line default
            #line hidden
            this.Write("\">\r\n");
            
            #line 56 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 if(!string.IsNullOrEmpty(package.Item2)) { 
            
            #line default
            #line hidden
            this.Write("\t\t\t <HintPath>");
            
            #line 57 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(package.Item2));
            
            #line default
            #line hidden
            this.Write("</HintPath>\r\n");
            
            #line 58 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    </Reference>\r\n");
            
            #line 60 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 61 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("  </ItemGroup>\r\n   <ItemGroup>\r\n");
            
            #line 64 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 foreach(var file in Model.AssemblyInfo.FilesToCompile) {
            
            #line default
            #line hidden
            this.Write("\t\t<compile Include=\"");
            
            #line 65 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Path.Combine(file.GenerationInfo.RelativePathFolder.Substring(Model.AssemblyInfo.ProjectFileRelativePath.Length,file.GenerationInfo.RelativePathFolder.Length-Model.AssemblyInfo.ProjectFileRelativePath.Length).TrimStart('\\'), file.GenerationInfo.FileName)));
            
            #line default
            #line hidden
            this.Write("\"/>\r\n");
            
            #line 66 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 }
            
            #line default
            #line hidden
            this.Write("  </ItemGroup>\r\n <ItemGroup>\r\n");
            
            #line 69 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 foreach(var import in Model.AssemblyInfo.AssemblyImports) {
            
            #line default
            #line hidden
            this.Write("\t<ProjectReference Include=\"..\\");
            
            #line 70 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Path.Combine(import.AssemblyInfo.ProjectFileRelativePath, import.AssemblyInfo.Name)));
            
            #line default
            #line hidden
            this.Write(".csproj\">\r\n      <Project>{");
            
            #line 71 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(import.AssemblyInfo.UniqueProjectId.ToString().ToUpper()));
            
            #line default
            #line hidden
            this.Write("}</Project>\r\n      <Name>");
            
            #line 72 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(import.AssemblyInfo.Name));
            
            #line default
            #line hidden
            this.Write("</Name>\r\n    </ProjectReference>\r\n");
            
            #line 74 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("  </ItemGroup>\r\n<ItemGroup>\r\n");
            
            #line 77 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 foreach(var include in Model.AssemblyInfo.IncludeOnlys) { 
            
            #line default
            #line hidden
            this.Write("    <Content Include=\"");
            
            #line 78 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(include));
            
            #line default
            #line hidden
            this.Write("\" />\r\n");
            
            #line 79 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 } 
            
            #line default
            #line hidden
            
            #line 80 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 foreach(var content in Model.AssemblyInfo.Contents) { 
            
            #line default
            #line hidden
            this.Write("    <Content Include=\"..\\");
            
            #line 81 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(content.OutputPath));
            
            #line default
            #line hidden
            this.Write("\">\r\n      <CopyToOutputDirectory>Always</CopyToOutputDirectory>\r\n    </Content>\r\n" +
                    "");
            
            #line 84 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"</ItemGroup>
  <Import Project=""$(MSBuildToolsPath)\Microsoft.CSharp.targets"" />
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
       Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name=""BeforeBuild"">
  </Target>
  <Target Name=""AfterBuild"">
  </Target>
  -->

 </Project>
");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "C:\Users\ogasi_000\Documents\Visual Studio 2015\Projects\Prototyper\CloudPrototyper\CloudPrototyper.NET.Framework.v462.Common\Templates\SolutionTemplates\Assemblies\LibraryAssemblyTemplate.tt"

private global::CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators.AssemblyFiles.LibraryAssemblyFileGenerator _ModelField;

/// <summary>
/// Access the Model parameter of the template.
/// </summary>
private global::CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators.AssemblyFiles.LibraryAssemblyFileGenerator Model
{
    get
    {
        return this._ModelField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool ModelValueAcquired = false;
if (this.Session.ContainsKey("Model"))
{
    this._ModelField = ((global::CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators.AssemblyFiles.LibraryAssemblyFileGenerator)(this.Session["Model"]));
    ModelValueAcquired = true;
}
if ((ModelValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Model");
    if ((data != null))
    {
        this._ModelField = ((global::CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators.AssemblyFiles.LibraryAssemblyFileGenerator)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public class LibraryAssemblyTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}