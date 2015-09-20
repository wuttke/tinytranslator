using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyTranslatorApplicationServer.Model;
using TinyTranslatorApplicationServer.Tasks;
using TinyTranslatorImportTranslationsClient.TinyTranslatorSyncService;

namespace TinyTranslatorImportTranslationsClient
{
    class Program
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        private TinyTranslatorSyncServiceClient service = new TinyTranslatorSyncServiceClient();

        static void Main(string[] args)
        {
            Program instance = new Program();
            instance.ImportTranslationsFromAssemblies(args);
        }

        private void ImportTranslationsFromAssemblies(string[] fileNames)
        {
            foreach (var fileName in fileNames)
                ImportTranslationsFromAssembly(fileName);
        }

        private void ImportTranslationsFromAssembly(string fileName)
        {
            String assemblyPath = Path.GetFullPath(fileName);
            Assembly ass = Assembly.LoadFile(assemblyPath);
            AssemblyTranslationCollector tc = new AssemblyTranslationCollector(1, ass, SyncTranslationCallback);
            var stats = tc.CollectTranslations();
            logger.Info("New translations: {0}, existing: {1}", stats.AddedTranslations, stats.ExistingTranslations);
        }

        private TranslationSyncStatistics SyncTranslationCallback(int projectID, string assemblyName, string bundleName, List<ResourceTranslation> translations)
        {
            logger.Info("Send {0} translations for asssembly {1}, bundle {2}",
                translations.Count, assemblyName, bundleName);

            var ra = new ResourceAssembly();
            ra.ProjectID = projectID;
            ra.FileName = assemblyName;

            var rb = new ResourceBundle();
            rb.ProjectID = projectID;
            rb.Name = bundleName;
            
            return service.SyncTranslations(ra, rb, translations);
        }
    }
}
