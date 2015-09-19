using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TinyTranslatorApplicationServer.Services
{
    public class TinyTranslatorImportExportService : ITinyTranslatorImportExportService
    {
        public void ImportTranslationsFromCsv(Stream csvFile)
        {
            throw new NotImplementedException();
        }

        public Stream ExportTranslationsToCsv(TranslationSelection translations)
        {
            throw new NotImplementedException();
        }

        public void ImportTranslationsFromAssembly(Stream assemblyFile)
        {
            throw new NotImplementedException();
        }

        public Stream ExportTranslationsToAssembly(TranslationSelection translations)
        {
            throw new NotImplementedException();
        }
    }
}
