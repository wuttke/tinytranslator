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

        void ExportTranslationsForBundle(ResourceBundle bundle, String locale, List<ResourceTranslation> translations);
        void FinishTranslationExport();

    }
}
