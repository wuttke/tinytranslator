using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TinyTranslatorApplicationServer.Model;
using TinyTranslatorApplicationServer.Tasks;

namespace TinyTranslatorApplicationServer.Services
{

    [ServiceContract]
    public interface ITinyTranslatorSyncService
    {

        /// <summary>
        /// Gets all projects.
        /// </summary>
        [OperationContract]
        ICollection<Project> GetProjects();

        /// <summary>
        /// Syncs a whole assembly with all bundles.
        /// Also processes deletions.
        /// This runs a long time and may require large arguments.
        /// </summary>
        [OperationContract]
        ResourceSyncStatistics SyncResourceAssembly(ResourceAssembly ra, ICollection<ResourceBundle> bundles);

        /// <summary>
        /// Syncs the assembly and the given bundles.
        /// No deletions are processed.
        /// Can be used if assembly is too large to be synced in one run.
        /// </summary>
        [OperationContract]
        ResourceSyncStatistics SyncBundles(ResourceAssembly ra, ICollection<ResourceBundle> bundles);

        /// <summary>
        /// Deletes all bundles of the given assembly which are NOT passed
        /// as existing bundles.
        /// </summary>
        [OperationContract]
        ResourceSyncStatistics SyncBundleDeletions(ResourceAssembly ra, List<String> existingBundleNames);

        /// <summary>
        /// Imports translations (not overwriting existing translations) to the given bundle.
        /// </summary>
        [OperationContract]
        TranslationSyncStatistics SyncTranslations(ResourceAssembly assembly, ResourceBundle bundle, List<ResourceTranslation> translations);

    }

}
