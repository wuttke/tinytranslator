using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using TinyTranslatorApplicationServer.Model;
using TinyTranslatorApplicationServer.Tasks;

namespace TinyTranslatorApplicationServer.Manager
{

    public class ImportTranslationsManager
    {

        private SyncTranslationsManager syncTranslationsManager = new SyncTranslationsManager();

        public TranslationSyncStatistics ImportTranslationsFromAssembly(int projectID, Stream assemblyStream)
        {
            var ass = new AssemblyUtil().GetAssemblyFromStream(assemblyStream);
            return ImportTranslationsFromAssembly(projectID, ass);
        }

        public TranslationSyncStatistics ImportTranslationsFromAssembly(int projectID, Assembly assembly)
        {
            var collector = new AssemblyTranslationCollector(projectID, assembly, SyncTranslationsCallback);
            return collector.CollectTranslations();
        }

        private TranslationSyncStatistics SyncTranslationsCallback(int projectID, string assemblyName, string bundleName, List<ResourceTranslation> translations)
        {
            var ra = new ResourceAssembly();
            ra.ProjectID = projectID;
            ra.FileName = assemblyName;

            var rb = new ResourceBundle();
            rb.ProjectID = projectID;
            rb.Name = bundleName;

            return syncTranslationsManager.SyncTranslations(ra, rb, translations);
        }

    }
}