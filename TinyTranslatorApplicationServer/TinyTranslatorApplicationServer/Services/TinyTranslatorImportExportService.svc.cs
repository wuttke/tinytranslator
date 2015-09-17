using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TinyTranslatorApplicationServer.Services
{
    public class TinyTranslatorImportExportService : ITinyTranslatorImportExportService
    {
        public void ImportTranslationsFromCsv(System.IO.Stream csvFile)
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream ExportTranslationsToCsv(TranslationSelection translations)
        {
            throw new NotImplementedException();
        }

        public void ImportTranslationsFromAssembly(System.IO.Stream assemblyFile)
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream ExportTranslationsToAssembly(TranslationSelection translations)
        {
            throw new NotImplementedException();
        }
    }
}
