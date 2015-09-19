using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Web;
using TinyTranslatorApplicationServer.Model;
using System.Data.Entity;

namespace TinyTranslatorApplicationServer.DAL
{
    public class ProjectRepository
    {

        private TinyTranslatorDbContext context;

        public ProjectRepository(TinyTranslatorDbContext context) 
        {
            this.context = context;
        }

        public ICollection<Project> GetProjects()
        {
            var projects = context.Projects.Include(x => x.Translations).ToList();
            return projects;
        }

        public Project GetProjectByID(int projectID)
        {
            return context.Projects.Include(x => x.Translations).Where(x => x.ID == projectID).First();
        }
    }
}