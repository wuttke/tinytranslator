using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.Model;
using System.Data.Entity;

namespace TinyTranslatorApplicationServer.DAL
{
    public class ResourceTranslationRepository
    {
        private TinyTranslatorDbContext context;

        public ResourceTranslationRepository(TinyTranslatorDbContext context) 
        {
            this.context = context;
        }

        public void AddTranslation(ResourceTranslation translation)
        {
            context.ResourceTranslations.Add(translation);
        }

        public List<ResourceTranslation> GetTranslationsForAssemblyAndLocale(int resourceAssemblyID, String locale)
        {
            return (from translation in context.ResourceTranslations.Include(t => t.Resource.ResourceBundle.ResourceAssembly)
                    where translation.ResourceAssemblyID == resourceAssemblyID
                      && translation.Locale == locale 
                    select translation).OrderBy(t => t.ResourceBundleID).ToList();
        }

        // TODO GetTranslations for TranslationSelection

        public void DeleteTranslation(ResourceTranslation translation)
        {
            context.ResourceTranslations.Remove(translation);
        }

        public void DeleteTranslationsForResource(Resource resource)
        {
            context.ResourceTranslations.RemoveRange(
                from translation in context.ResourceTranslations
                where translation.ResourceID == resource.ID
                select translation
                );
        }

    }
}