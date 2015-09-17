using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.DAL
{
    public class ResourceBundleRepository
    {

        private TinyTranslatorDbContext context;

        public ResourceBundleRepository(TinyTranslatorDbContext context) 
        {
            this.context = context;
        }

        public List<ResourceBundle> FindBundlesForAssembly(ResourceAssembly ra)
        {
            var query = from rb in context.ResourceBundles where rb.ResourceAssemblyID == ra.ID select rb;
            return query.ToList();
        }

        public void AddBundle(ResourceBundle bundle)
        {
            context.ResourceBundles.Add(bundle);
        }

        public TranslationStatus GetWorstTranslationStatusForAssemblyFromBundles(ResourceAssembly ra)
        {
            return (from bundle in context.ResourceBundles
                    where bundle.ResourceAssemblyID == ra.ID
                        && bundle.WorstTranslationStatus >= 0
                        && bundle.BundleSyncStatus != BundleSyncStatus.REMOVED
                    select bundle.WorstTranslationStatus).Min();
            // was passiert, wenn kein Status da ist, um Min() zu machen?
        }

        // Statistiken?

    }
}