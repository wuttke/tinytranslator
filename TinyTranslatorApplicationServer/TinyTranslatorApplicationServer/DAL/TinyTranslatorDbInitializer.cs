using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.DAL
{
    public class TinyTranslatorDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TinyTranslatorDbContext>
    {
        protected override void Seed(TinyTranslatorDbContext context)
        {
            var project = new Project();
            project.Name = "Meona";
            project.ProjectLocale = "de_DE";
            context.Projects.Add(project);

            ProjectLocale pl = new ProjectLocale();
            pl.LocaleCode = "en_US";
            pl.Project = project;
            context.ProjectLocales.Add(pl);

            context.SaveChanges();

            var assembly = new ResourceAssembly();
            assembly.CreateDateTime = DateTime.UtcNow;
            assembly.LastSyncDateTime = DateTime.UtcNow;
            assembly.FileName = "MeonaTest";
            assembly.ProjectID = project.ID;
            context.ResourceAssemblies.Add(assembly);

            //var bundle = new ResourceBundle();
            //bundle.Name = "MeonaChart.application.editors.VitalParameterEditor2, MeonaChart";

            context.SaveChanges();
        }
    }
}