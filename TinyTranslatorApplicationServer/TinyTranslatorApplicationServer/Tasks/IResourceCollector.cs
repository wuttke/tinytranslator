using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Tasks
{

    interface IResourceCollector
    {
        ResourceSyncStatistics Statistics { get; }

        void CollectResourceAssembly();
        void CollectResourceBundles();
        void DeleteBundles();
    }

    public delegate ResourceSyncStatistics BundleSyncCallback(ResourceAssembly assembly, ResourceBundle bundle);
    public delegate ResourceSyncStatistics BundleDeleteCallback(ResourceAssembly assembly, List<String> existingAssemblies);

}
