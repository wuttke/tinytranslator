using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Web;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Tasks
{
    public class AssemblyTranslationsExporter : ITranslationExporter
    {

        private String outputDir;
        private String assemblyName;
        private String locale;
        private ModuleBuilder moduleBuilder;
        private AssemblyBuilder assemblyBuilder;

        public AssemblyTranslationsExporter(String outputDir, String assemblyName, String locale)
        {
            this.outputDir = outputDir;
            this.assemblyName = assemblyName;
            this.locale = locale;

            CreateAssemblyBuilder();
        }

        private void CreateAssemblyBuilder()
        {
            var assName = new AssemblyName();
            assName.CultureInfo = new CultureInfo(locale);
            assName.Name = assemblyName + ".resources"; // XXX.resources.dll
            assName.CodeBase = outputDir;

            var domain = Thread.GetDomain();
            assemblyBuilder = domain.DefineDynamicAssembly(assName, AssemblyBuilderAccess.Save, outputDir);

            String fileName = assName.Name + ".dll";
            moduleBuilder = assemblyBuilder.DefineDynamicModule(fileName, fileName);
        }

        public void ExportTranslationsForBundle(ResourceBundle bundle, String locale, List<ResourceTranslation> translations)
        {
            String resourceName = bundle.Name + "." + locale + ".resources";
            var writer = moduleBuilder.DefineResource(resourceName, "", ResourceAttributes.Public);
            foreach (var translation in translations)
            {
                Debug.Assert(translation.Locale == locale);
                Debug.Assert(bundle.ID == translation.ResourceBundleID);
                if (translation.Resource.ResourceType == ResourceType.STRING)
                    writer.AddResource(translation.Resource.Key, translation.StringValue);
                else if (translation.Resource.ResourceType == ResourceType.IMAGE)
                    writer.AddResource(translation.Resource.Key, Image.FromStream(new MemoryStream(translation.BinaryValue)));
                else
                {
                    // may need to deserialize to Object first (this way it seems meta-information is lost)
                    writer.AddResource(translation.Resource.Key, translation.BinaryValue);
                }
            }
        }

        public void FinishTranslationExport()
        {
            assemblyBuilder.Save(assemblyName + ".resources.dll");
        }

    }
}