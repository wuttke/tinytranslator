using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Tasks
{
    interface ITranslationCollector
    {
        TranslationSyncStatistics CollectTranslations();
    }

    public delegate TranslationSyncStatistics TranslationSyncCallback(int projectID, String assemblyName, String bundleName, List<ResourceTranslation> translations);

}
