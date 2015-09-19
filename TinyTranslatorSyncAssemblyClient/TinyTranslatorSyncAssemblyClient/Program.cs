using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TinyTranslatorApplicationServer.Model;
using TinyTranslatorApplicationServer.Tasks;
using TinyTranslatorSyncAssemblyClient.TinyTranslatorSyncService;

namespace TinyTranslatorSyncAssemblyClient
{
    class Program
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Program instance;
        private TinyTranslatorSyncServiceClient service;

        static void Main(string[] args)
        {
            instance = new Program();
            instance.ProcessFiles(args);
        }

        private void ProcessFiles(string[] args)
        {
            service = new TinyTranslatorSyncServiceClient();
            foreach (String fileName in args)
                ProcessFile(fileName, service);
            logger.Info("Program finished successfully");
        }

        private void ProcessFile(string fileName, TinyTranslatorSyncServiceClient service)
        {
            logger.Info("Parse file {0}", fileName);
            String assemblyPath = Path.GetFullPath(fileName);

            Assembly ass = Assembly.LoadFile(assemblyPath);

            ResourceCollector rc = new ResourceCollector(ass, CallSyncBundle, CallDeleteBundle);
            rc.CollectResourceAssembly();
            rc.CollectResourceBundles();
            rc.DeleteBundles();
            var statistics = rc.Statistics;

            // Variant that transmits whole DLL
            //var statistics = service.ImportResourceFromAssembly(new FileStream(fileName, FileMode.Open));

            logger.Info("Bundles: {0}/{1}/{2}, Resources: {3}/{4}/{5}",
                statistics.AddedBundles, statistics.UpdatedBundles, statistics.RemovedBundles,
                statistics.AddedResources, statistics.UpdatedResources, statistics.RemovedResources);
        }

        private ResourceSyncStatistics CallDeleteBundle(ResourceAssembly assembly, List<string> existingAssemblies)
        {
            return service.SyncBundleDeletions(assembly, existingAssemblies);
        }

        private ResourceSyncStatistics CallSyncBundle(ResourceAssembly assembly, ResourceBundle bundle)
        {
            var bundles = new List<ResourceBundle>();
            if (bundle != null)
                bundles.Add(bundle);
            return service.SyncBundles(assembly, bundles);
        }

    }
}
