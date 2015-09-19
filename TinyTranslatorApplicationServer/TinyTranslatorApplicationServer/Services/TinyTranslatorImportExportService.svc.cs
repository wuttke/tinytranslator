using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TinyTranslatorApplicationServer.Manager;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Services
{
    public class TinyTranslatorImportExportService : ITinyTranslatorImportExportService
    {

        #region Resources
        private ImportResourcesManager importResourcesManager = new ImportResourcesManager();

        public ResourceSyncStatistics ImportResourceFromAssembly(Stream assemblyStream)
        {
            int projectID = 1; // TODO!
            return importResourcesManager.ImportResourcesFromAssembly(projectID, assemblyStream);
        }
        #endregion

        #region Translations
        private ImportTranslationsManager importTranslationsManager = new ImportTranslationsManager();

        public TranslationSyncStatistics ImportTranslationsFromCsv(Stream csvFile)
        {
            throw new NotImplementedException();
        }

        public Stream ExportTranslationsToCsv(TranslationSelection translations)
        {
            throw new NotImplementedException();
        }

        public TranslationSyncStatistics ImportTranslationsFromAssembly(Stream assemblyStream)
        {
            int projectID = 1; // TODO!!
            return importTranslationsManager.ImportTranslationsFromAssembly(projectID, assemblyStream);
        }

        public Stream ExportTranslationsToAssembly(TranslationSelection translations)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
