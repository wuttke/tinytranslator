using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.Model;
using TrackerEnabledDbContext;

namespace TinyTranslatorApplicationServer.DAL
{
    public class TinyTranslatorDbContext : TrackerContext //DbContext
    {

        public TinyTranslatorDbContext()
            : base("TinyTranslatorDbContext") //TinyTranslatorDb?
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectLocale> ProjectLocales { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceAssembly> ResourceAssemblies { get; set; }
        public DbSet<ResourceBundle> ResourceBundles { get; set; }
        public DbSet<ResourceTranslation> ResourceTranslations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}