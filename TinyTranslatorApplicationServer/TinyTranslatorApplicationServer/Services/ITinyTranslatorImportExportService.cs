using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TinyTranslatorApplicationServer.DAL;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Services
{
    [ServiceContract]
    public interface ITinyTranslatorImportExportService
    {

        /// <summary>
        /// Lists all projects.
        /// </summary>
        [OperationContract]
        ICollection<Project> GetProjects();

        /// <summary>
        /// Lists all assemblies of a project.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [OperationContract]
        ICollection<ResourceAssembly> GetResourceAssembliesByProject(int projectID);

        /// <summary>
        /// Imports resources from a whole assembly with all bundles.
        /// Also processes deletions.
        /// This runs a long time. (TODO maybe offer background call w/out statistics)
        /// </summary>
        // TODO Project ID via Web Param?
        [OperationContract]
        ResourceSyncStatistics ImportResourceFromAssembly(Stream assemblyStream);

        [OperationContract]
        TranslationSyncStatistics ImportTranslationsFromCsv(Stream csvFile);
        
        [OperationContract]
        Stream ExportTranslationsToCsv(TranslationSelection translations);

        /// <summary>
        /// Imports translations from the given satellite assembly.
        /// </summary>
        // TODO Project ID via Web Param?
        [OperationContract]
        TranslationSyncStatistics ImportTranslationsFromAssembly(Stream assemblyStream);
        
        /// <summary>
        /// Creates a satellite assembly for the given translations.
        /// </summary>
        [OperationContract]
        Stream ExportTranslationsToAssembly(int resourceAssemblyID, String locale);

    }
}
