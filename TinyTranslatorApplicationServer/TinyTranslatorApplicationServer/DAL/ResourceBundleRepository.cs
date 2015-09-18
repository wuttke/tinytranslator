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
            var status = (from bundle in context.ResourceBundles
                    where bundle.ProjectID == ra.ProjectID
                        && bundle.ResourceAssemblyID == ra.ID
                        && bundle.WorstTranslationStatus >= 0
                        && bundle.BundleSyncStatus != BundleSyncStatus.REMOVED
                    select (TranslationStatus?)bundle.WorstTranslationStatus).Min();

            return status.HasValue ? status.Value : TranslationStatus.NO_NEED_TO_TRANSLATE;
        }

        public ResourceBundle FindBundleByName(int projectID, int assemblyID, string bundleName)
        {
            return (from bundle in context.ResourceBundles
                    where bundle.ProjectID == projectID
                        && bundle.ResourceAssemblyID == assemblyID
                        && bundle.Name == bundleName
                    select bundle).FirstOrDefault();
        }

    }
}