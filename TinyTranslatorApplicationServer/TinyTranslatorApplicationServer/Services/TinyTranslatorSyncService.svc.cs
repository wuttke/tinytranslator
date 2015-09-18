using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TinyTranslatorApplicationServer.DAL;
using TinyTranslatorApplicationServer.Model;
using TinyTranslatorApplicationServer.Sync;

namespace TinyTranslatorApplicationServer.Services
{

    public class TinyTranslatorSyncService : ITinyTranslatorSyncService
    {

        // TODO Unit Tests, IOC, EF Tracing, Security (Users!), "echte" DB

        public ICollection<Project> GetProjects()
        {
            var context = new TinyTranslatorDbContext();
            var projects = context.Projects.Include(x => x.Translations).ToList();
            return projects;
        }

        public ResourceBundle GetResourceBundle(int bundleID)
        {
            var context = new TinyTranslatorDbContext();
            context.ResourceBundles.Include("Resources.Translations").Where(rb => rb.ID == bundleID).Single();
            return context.ResourceBundles.Find(bundleID);
        }

        public SyncStatistics SyncResourceAssembly(ResourceAssembly ra, ICollection<ResourceBundle> bundles)
        {
            return InnerSyncResourceAssembly(ra, bundles, true);
        }

        public SyncStatistics SyncBundles(ResourceAssembly ra, ICollection<ResourceBundle> bundles)
        {
            return InnerSyncResourceAssembly(ra, bundles, deleteBundles: false);
        }

        private SyncStatistics InnerSyncResourceAssembly(ResourceAssembly ra, ICollection<ResourceBundle> bundles, bool deleteBundles)
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

        public SyncStatistics SyncBundleDeletions(ResourceAssembly ra, List<string> existingBundleNames)
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
