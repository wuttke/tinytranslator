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
using TinyTranslatorSyncAssemblyClient.TinyTranslatorApplicationServer;

namespace TinyTranslatorSyncAssemblyClient
{
    class Program
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var service = new TinyTranslatorResourceServiceClient();
            foreach (String fileName in args)
            {
                logger.Info("Parse file {0}", fileName);
                String assemblyPath = Path.GetFullPath(fileName);
                ResourceAssembly assembly = new ResourceAssembly();
                List<ResourceBundle> bundles = new List<ResourceBundle>();
                ParseAssemblyAndBundles(assemblyPath, assembly, bundles);

                logger.Info("Transmit {0} bundles to server", bundles.Count);

                var statistics = service.SyncResourceAssembly(assembly, bundles);

                logger.Info("Bundles: {0}/{1}/{2}, Resources: {3}/{4}/{5]",
                    statistics.AddedBundles, statistics.UpdatedBundles, statistics.RemovedBundles,
                    statistics.AddedResources, statistics.UpdatedResources, statistics.RemovedResources);
            }
            logger.Info("Program finished successfully");
        }

        private static void ParseAssemblyAndBundles(string fileName, ResourceAssembly assembly, List<ResourceBundle> bundles)
        {
            Assembly ass = Assembly.LoadFile(fileName);

            AssemblyBundleCollector collector = new AssemblyBundleCollector(ass);
            collector.CollectAssembly(assembly);
            collector.CollectBundles(bundles);
        }

    }
}
