using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TinyTranslatorApplicationServer.DAL;
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
        private ExportTranslationsManager exportTranslationsManager = new ExportTranslationsManager();

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

        public Stream ExportTranslationsToAssembly(int resourceAssemblyID, String locale)
        {
            return exportTranslationsManager.ExportTranslationsAsAssembly(resourceAssemblyID, locale);
        }
        #endregion

    }
}
