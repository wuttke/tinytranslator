using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.DAL;
using TinyTranslatorApplicationServer.Model;
using TinyTranslatorApplicationServer.Tasks;

namespace TinyTranslatorApplicationServer.Manager
{
    public class ExportTranslationsManager
    {

        public Stream ExportTranslationsAsAssembly(int resourceAssemblyID, String locale)
        {
            var context = new TinyTranslatorDbContext();
            var translationsRepository = new ResourceTranslationRepository(context);
            var translations = translationsRepository.GetTranslationsForAssemblyAndLocale(resourceAssemblyID, locale);
            return ExportTranslationsAsAssembly(translations, locale);
        }

        private Stream ExportTranslationsAsAssembly(List<ResourceTranslation> translations, String locale)
        {
            if (translations.Count == 0)
                return new MemoryStream();

            String outDir = Path.GetTempPath();
            String assemblyName = translations[0].Resource.ResourceBundle.ResourceAssembly.FileName;
            ITranslationExporter exporter = new AssemblyTranslationsExporter(outDir, assemblyName, locale);
            foreach (var bundle in translations.Select(t => t.Resource.ResourceBundle).Distinct())
            {
                var bundleTranslations = translations.Where(t => t.Resource.ResourceBundle == bundle).ToList();
                exporter.ExportTranslationsForBundle(bundle, locale, bundleTranslations);
            }
            exporter.FinishTranslationExport();
            return new FileStream(outDir + Path.DirectorySeparatorChar + assemblyName + ".resources.dll", FileMode.Open);
        }

    }
}