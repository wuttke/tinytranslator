using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.DAL
{
    public class ResourceAssemblyRepository
    {

        private TinyTranslatorDbContext context;

        public ResourceAssemblyRepository(TinyTranslatorDbContext context) 
        {
            this.context = context;
        }

        public ResourceAssembly FindAssemblyByName(int projectID, String assemblyName)
        {
            var result = from ra in context.ResourceAssemblies 
                         where ra.ProjectID == projectID && ra.FileName == assemblyName 
                         select ra;
            return result.FirstOrDefault();
        }

        public void AddAssembly(ResourceAssembly newAssembly)
        {
            context.ResourceAssemblies.Add(newAssembly);
        }
    }
}