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
using TinyTranslatorApplicationServer.Tasks;

namespace TinyTranslatorApplicationServer.Services
{

    public class TinyTranslatorSyncService : ITinyTranslatorSyncService
    {

        // TODO Unit Tests, IOC, EF Tracing, Security (Users!), "echte" DB

        public ICollection<Project> GetProjects()
        {
            var context = new TinyTranslatorDbContext();
            return new ProjectRepository(context).GetProjects();
        }

        #region Resources
        private ImportResourcesManager importResourcesManager = new ImportResourcesManager();
        private SyncResourcesManager syncResourcesManager = new SyncResourcesManager();

        public ResourceSyncStatistics ImportResourceFromAssembly(Stream assemblyStream)
        {
            int projectID = 1; // TODO!
            return importResourcesManager.ImportResourcesFromAssembly(projectID, assemblyStream);
        }

        public ResourceSyncStatistics SyncResourceAssembly(ResourceAssembly ra, ICollection<ResourceBundle> bundles)
        {
            return syncResourcesManager.SyncResourceAssembly(ra, bundles);
        }

        public ResourceSyncStatistics SyncBundles(ResourceAssembly ra, ICollection<ResourceBundle> bundles)
        {
            return syncResourcesManager.SyncBundles(ra, bundles);
        }

        public ResourceSyncStatistics SyncBundleDeletions(ResourceAssembly ra, List<string> existingBundleNames)
        {
            return syncResourcesManager.SyncBundleDeletions(ra, existingBundleNames);
        }
        #endregion

        #region Translations
        private ImportTranslationsManager importTranslationsManager = new ImportTranslationsManager();
        private SyncTranslationsManager syncTranslationsManager = new SyncTranslationsManager();

        public TranslationSyncStatistics ImportTranslationsFromAssembly(Stream assemblyStream)
        {
            int projectID = 1; // TODO!!
            return importTranslationsManager.ImportTranslationsFromAssembly(projectID, assemblyStream);
        }

        public TranslationSyncStatistics SyncTranslations(ResourceAssembly assembly, ResourceBundle bundle, List<ResourceTranslation> translations)
        {
            return syncTranslationsManager.SyncTranslations(assembly, bundle, translations);
        }
        #endregion
    }
}
