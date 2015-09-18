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
using TinyTranslatorSyncAssemblyClient.TinyTranslatorSyncService;

namespace TinyTranslatorSyncAssemblyClient
{
    class Program
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var service = new TinyTranslatorSyncServiceClient();
            foreach (String fileName in args)
                ProcessFile(fileName, service);
            logger.Info("Program finished successfully");
        }

        private static void ProcessFile(string fileName, TinyTranslatorSyncServiceClient service)
        {
            logger.Info("Parse file {0}", fileName);
            String assemblyPath = Path.GetFullPath(fileName);
            Assembly ass = Assembly.LoadFile(assemblyPath);

            AssemblyBundleCollector collector = new AssemblyBundleCollector(ass, service);
            collector.CollectAssembly();
            collector.CollectBundles();
            collector.DeleteBundles();

            var statistics = collector.Statistics;
            logger.Info("Bundles: {0}/{1}/{2}, Resources: {3}/{4}/{5]",
                statistics.AddedBundles, statistics.UpdatedBundles, statistics.RemovedBundles,
                statistics.AddedResources, statistics.UpdatedResources, statistics.RemovedResources);
        }

    }
}
