namespace TinyTranslatorApplicationServer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TinyTranslatorApplicationServer.DAL;
    using TinyTranslatorApplicationServer.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<TinyTranslatorDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TinyTranslatorApplicationServer.DAL.TinyTranslatorDbContext";
        }

        protected override void Seed(TinyTranslatorDbContext context)
        {
            /*
            context.Projects.AddOrUpdate(p => p.Name,
                new Project { Name="Meona" });
             */
        }
    }
}
