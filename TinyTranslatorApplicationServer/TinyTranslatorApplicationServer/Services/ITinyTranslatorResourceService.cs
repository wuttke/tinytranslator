using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TinyTranslatorApplicationServer.Model;
using TinyTranslatorApplicationServer.Sync;

namespace TinyTranslatorApplicationServer.Services
{

    [ServiceContract]
    public interface ITinyTranslatorSyncService
    {

        [OperationContract]
        ICollection<Project> GetProjects();

        [OperationContract]
        ResourceBundle GetResourceBundle(int bundleID);
        
        /// <summary>
        /// Syncs a whole assembly with all bundles.
        /// Also processes deletions.
        /// </summary>
        [OperationContract]
        SyncStatistics SyncResourceAssembly(ResourceAssembly ra, ICollection<ResourceBundle> bundles);

        /// <summary>
        /// Syncs the assembly and the given bundles.
        /// No deletions are processed.
        /// Can be used if assembly is too large to be synced in one run.
        /// </summary>
        [OperationContract]
        SyncStatistics SyncBundles(ResourceAssembly ra, ICollection<ResourceBundle> bundles);

        /// <summary>
        /// Deletes all bundles of the given assembly which are NOT passed
        /// as existing bundles.
        /// </summary>
        [OperationContract]
        SyncStatistics SyncBundleDeletions(ResourceAssembly ra, List<String> existingBundleNames);

    }

}
