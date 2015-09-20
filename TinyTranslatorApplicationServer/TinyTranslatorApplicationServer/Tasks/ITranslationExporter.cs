using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Tasks
{
    interface ITranslationExporter
    {
        
        void StartTranslationExport(Project project, String locale, ResourceAssembly assembly);
        void ExportTranslationsForBundle(ResourceBundle bundle, List<ResourceTranslation> translations);
        void FinishTranslationExport();

    }
}
