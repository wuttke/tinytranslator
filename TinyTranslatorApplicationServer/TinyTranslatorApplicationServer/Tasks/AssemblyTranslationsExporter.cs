using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Tasks
{
    public class AssemblyTranslationsExporter : ITranslationExporter
    {

        private String outputDir;
        private String assemblyName;
        private String locale;
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
            assName.Name = assemblyName;
            assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assName, AssemblyBuilderAccess.Save);
        }

        public void ExportTranslationsForBundle(ResourceBundle bundle, String locale, List<ResourceTranslation> translations)
        {
            String resourceName = bundle.Name + "." + locale;
            var writer = assemblyBuilder.DefineResource(resourceName, "", resourceName + ".resources");
            foreach (var translation in translations)
            {
                Debug.Assert(translation.Locale == locale);
                Debug.Assert(bundle.ID == translation.ResourceBundleID);
                writer.AddResource(translation.Resource.Key, translation.BinaryValue);
            }
            writer.Close();
        }

        public void FinishTranslationExport()
        {
            String od = outputDir;
            if (!od.EndsWith(Path.DirectorySeparatorChar.ToString()))
                od += Path.DirectorySeparatorChar;
            assemblyBuilder.Save(od + assemblyName + ".dll");
        }

    }
}