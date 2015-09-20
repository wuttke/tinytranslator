using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyTranslatorExportTranslationsClient.TinyTranslatorImportExportService;

namespace TinyTranslatorExportTranslationsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            String outDir = "";
            if (args.Length > 0)
                outDir = args[0];

            int projectID = 1; // arg?

            var service = new TinyTranslatorImportExportServiceClient();
            var projects = service.GetProjects();
            var project = projects.FirstOrDefault(p => p.ID == projectID);
            var assemblies = service.GetResourceAssembliesByProject(projectID);

            foreach (var locale in project.Translations)
            {
                Console.WriteLine("Export locale: {0}", locale.LocaleCode);
                String assDir = outDir.Length > 0 ? outDir + Path.DirectorySeparatorChar + locale.LocaleCode : locale.LocaleCode;
                Directory.CreateDirectory(assDir);
                
                foreach (var assembly in assemblies)
                {
                    var fileName = assDir + Path.DirectorySeparatorChar + assembly.FileName + ".dll";
                    
                    Console.WriteLine("Export assembly: {0}", assembly.FileName);
                    var assStream = service.ExportTranslationsToAssembly(assembly.ID, locale.LocaleCode);
                    var assFileStream = new FileStream(fileName, FileMode.Create);
                    assStream.CopyTo(assFileStream);
                    assStream.Close();
                    assFileStream.Close();

                    if (new FileInfo(fileName).Length == 0)
                    {
                        File.Delete(fileName);
                        Console.WriteLine("No translations found.");
                    }
                }
            }
        }
    }
}
