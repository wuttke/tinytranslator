using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.DAL;
using TinyTranslatorApplicationServer.Model;
using TinyTranslatorApplicationServer.Tasks;

namespace TinyTranslatorApplicationServer.Manager
{
    public class SyncTranslationsManager
    {

        public TranslationSyncStatistics SyncTranslations(ResourceAssembly assembly, ResourceBundle bundle, List<ResourceTranslation> translations)
        {
            var context = new TinyTranslatorDbContext();

            var task = new SyncTranslationsTask(
                new ProjectRepository(context),
                new ResourceAssemblyRepository(context),
                new ResourceBundleRepository(context),
                new ResourceRepository(context),
                new ResourceTranslationRepository(context)
                );

            var existingAssembly = task.SyncTranslations(assembly, bundle, translations);
            context.SaveChanges();

            if (existingAssembly != null)
            {
                task.CalculateAssemblyStatusFromBundles(existingAssembly);
                context.SaveChanges();
            }

            return task.Statistics;
        }

    }
}