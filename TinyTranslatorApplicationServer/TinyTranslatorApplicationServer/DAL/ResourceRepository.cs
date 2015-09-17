using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.DAL
{
    public class ResourceRepository
    {

        private TinyTranslatorDbContext context;

        public ResourceRepository(TinyTranslatorDbContext context) 
        {
            this.context = context;
        }

        public void AddResource(Resource resource)
        {
            context.Resources.Add(resource);
        }

        public List<Resource> GetResourcesForBundle(ResourceBundle bundle)
        {
            return (from res in context.Resources where res.ResourceBundleID == bundle.ID select res).ToList();
        }
    }
}