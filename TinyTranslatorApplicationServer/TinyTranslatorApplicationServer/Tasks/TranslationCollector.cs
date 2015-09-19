using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Tasks
{

    /// <summary>
    /// Collects translations from a satellite assembly.
    /// </summary>
    public class TranslationCollector
    {

        private const String TRANSLATION_IMPORTER = "importer"; // TODO durch aktuellen Nutzer ersetzen?
        private const TranslationStatus COLLECT_STATUS = TranslationStatus.IMPORTED; // TODO konfigurierbar?

        private int projectID;
        private Assembly assembly;
        private TranslationSyncCallback translationSyncCallback;
        private TranslationSyncStatistics statistics = new TranslationSyncStatistics();

        public TranslationCollector(int projectID, Assembly assembly, TranslationSyncCallback translationSyncCallback)
        {
            this.projectID = projectID;
            this.assembly = assembly;
            this.translationSyncCallback = translationSyncCallback;
        }

        public TranslationSyncStatistics CollectTranslations()
        {
            String assemblyName = assembly.GetName().Name;
            assemblyName = assemblyName.Substring(0, assemblyName.LastIndexOf('.')); // strip ".resources"

            var bundleNames = assembly.GetManifestResourceNames();

            foreach (var bundleName in bundleNames)
            {
                if (bundleName.EndsWith(".resources"))
                    CollectBundle(assemblyName, bundleName);
            }

            return statistics;
        }

        private void CollectBundle(string assemblyName, string bundleName)
        {
            String strippedBundleName = bundleName.Substring(0, bundleName.Length - ".resources".Length); // Strip ".resources" postfix
            String locale = strippedBundleName.Substring(strippedBundleName.LastIndexOf('.') + 1);
            strippedBundleName = strippedBundleName.Substring(0, strippedBundleName.LastIndexOf('.')); // Strip "locale" postfix

            var translations = new List<ResourceTranslation>();

            var stream = assembly.GetManifestResourceStream(bundleName);
            var reader = new ResourceReader(stream);
            IDictionaryEnumerator dict = reader.GetEnumerator();
            while (dict.MoveNext())
                CollectTranslatedResource(strippedBundleName, dict, reader, translations);

            if (translations.Count > 0)
            {
                var stats = translationSyncCallback(projectID, assemblyName, strippedBundleName, translations);
                AddStats(stats);
            }
        }

        private void CollectTranslatedResource(string bundleName, IDictionaryEnumerator dict, ResourceReader reader, List<ResourceTranslation> translations)
        {
            Object dictValue = null;
            try
            {
                dictValue = dict.Value;
            }
            catch (Exception)
            {
                // wenn eine Assembly wie MeonaKernel gebraucht wird, um dict.Value aufzulösen
                return;
            }
           
            String key = dict.Key.ToString();
            if (key.StartsWith(">>"))
                return;

            String type;
            byte[] data;
            reader.GetResourceData(dict.Key.ToString(), out type, out data);

            ResourceTranslation translation = new ResourceTranslation();
            translation.BinaryValue = data;
            translation.StringValue = dict.Value != null ? dict.Value.ToString() : null;
            translation.TranslationDateTime = DateTime.UtcNow;
            translation.TranslationStatus = COLLECT_STATUS;
            translation.TranslationBy = TRANSLATION_IMPORTER;

            if (translation.StringValue != null || translation.BinaryValue != null)
            {
                translation.Locale = assembly.GetName().CultureInfo.Name;
                translation.Resource = new Resource();
                translation.Resource.Key = key;
                translation.Resource.ResourceClass = type;

                translations.Add(translation);
            }
        }

        private void AddStats(TranslationSyncStatistics stats)
        {
            statistics.AddedTranslations += stats.AddedTranslations;
            statistics.ExistingTranslations += stats.ExistingTranslations;
            statistics.ResourceMismatch += stats.ResourceMismatch;
            statistics.ResourceNotFound += stats.ResourceNotFound;
            statistics.UnchangedResource += stats.UnchangedResource;
        }

    }

    public delegate TranslationSyncStatistics TranslationSyncCallback(int projectID, String assemblyName, String bundleName, List<ResourceTranslation> translations);

}