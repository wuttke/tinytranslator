using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using TinyTranslatorApplicationServer.Model;
using TinyTranslatorApplicationServer.Services;
using TinyTranslatorApplicationServer.Tasks;

namespace TinyTranslatorApplicationServer.Manager
{

    public class ImportResourcesManager
    {

        private SyncResourcesManager syncResourcesManager = new SyncResourcesManager();

        public ResourceSyncStatistics ImportResourcesFromAssembly(int projectID, Stream assemblyStream)
        {
            var assembly = new AssemblyUtil().GetAssemblyFromStream(assemblyStream);
            return ImportResourcesFromAssembly(projectID, assembly);
        }

        public ResourceSyncStatistics ImportResourcesFromAssembly(int projectID, Assembly assembly)
        {
            IResourceCollector collector = new AssemblyResourceCollector(assembly, SyncBundleCallback, DeleteBundlesClassback);
            return ImportResourcesWithCollector(collector);
        }

        // TODO ImportFromPot etc.

        private ResourceSyncStatistics ImportResourcesWithCollector(IResourceCollector collector)
        {
            collector.CollectResourceAssembly();
            collector.CollectResourceBundles();
            collector.DeleteBundles();
            return collector.Statistics;
        }

        private ResourceSyncStatistics DeleteBundlesClassback(ResourceAssembly assembly, List<string> existingAssemblies)
        {
            return syncResourcesManager.SyncBundleDeletions(assembly, existingAssemblies);
        }

        private ResourceSyncStatistics SyncBundleCallback(ResourceAssembly assembly, ResourceBundle bundle)
        {
            var bundles = new List<ResourceBundle>();
            if (bundle != null)
                bundles.Add(bundle);
            return syncResourcesManager.SyncBundles(assembly, bundles);
        }

    }
}