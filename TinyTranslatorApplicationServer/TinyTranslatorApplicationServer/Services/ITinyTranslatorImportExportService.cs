using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Services
{
    [ServiceContract]
    public interface ITinyTranslatorImportExportService
    {

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
        
        [OperationContract]
        Stream ExportTranslationsToAssembly(TranslationSelection translations);

    }
}
