using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.DAL;
using TinyTranslatorApplicationServer.Model;
using TinyTranslatorApplicationServer.Tasks;

namespace TinyTranslatorApplicationServer.Manager
{

    /// <summary>
    /// Singleton
    /// </summary>
    public class SyncResourcesManager
    {

        public ResourceSyncStatistics SyncResourceAssembly(ResourceAssembly ra, ICollection<ResourceBundle> bundles)
        {
            return InnerSyncResourceAssembly(ra, bundles, true);
        }

        public ResourceSyncStatistics SyncBundles(ResourceAssembly ra, ICollection<ResourceBundle> bundles)
        {
            return InnerSyncResourceAssembly(ra, bundles, deleteBundles: false);
        }

        private ResourceSyncStatistics InnerSyncResourceAssembly(ResourceAssembly ra, ICollection<ResourceBundle> bundles, bool deleteBundles)
        {
            var context = new TinyTranslatorDbContext();

            var task = new SyncResourcesTask(
                new ResourceAssemblyRepository(context),
                new ResourceBundleRepository(context),
                new ResourceRepository(context),
                new ResourceTranslationRepository(context)
                );

            var assembly = task.SyncResourceAssembly(ra);
            if (deleteBundles)
                task.SyncAllBundlesWithDeletions(assembly, bundles);
            else
                task.SyncSomeBundles(assembly, bundles);
            context.SaveChanges();

            task.CalculateAssemblyStatusFromBundles(assembly);
            context.SaveChanges();

            return task.Statistics;
        }

        public ResourceSyncStatistics SyncBundleDeletions(ResourceAssembly ra, List<string> existingBundleNames)
        {
            var context = new TinyTranslatorDbContext();

            var task = new SyncResourcesTask(
                new ResourceAssemblyRepository(context),
                new ResourceBundleRepository(context),
                new ResourceRepository(context),
                new ResourceTranslationRepository(context)
                );

            var assembly = task.SyncResourceAssembly(ra);
            task.DeleteNonExistingBundles(assembly, existingBundleNames);
            context.SaveChanges();

            task.CalculateAssemblyStatusFromBundles(assembly);
            context.SaveChanges();

            return task.Statistics;
        }

    }
}