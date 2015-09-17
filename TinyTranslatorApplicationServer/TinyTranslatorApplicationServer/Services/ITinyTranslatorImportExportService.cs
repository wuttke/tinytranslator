using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TinyTranslatorApplicationServer.Services
{
    [ServiceContract]
    public interface ITinyTranslatorImportExportService
    {

        [OperationContract]
        void ImportTranslationsFromCsv(Stream csvFile);
        
        [OperationContract]
        Stream ExportTranslationsToCsv(TranslationSelection translations);

        [OperationContract]
        void ImportTranslationsFromAssembly(Stream assemblyFile);
        
        [OperationContract]
        Stream ExportTranslationsToAssembly(TranslationSelection translations);

    }
}
