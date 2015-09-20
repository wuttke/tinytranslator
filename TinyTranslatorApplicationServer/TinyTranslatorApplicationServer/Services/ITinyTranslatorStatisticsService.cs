using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TinyTranslatorApplicationServer.DAL;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Services
{
        
    [ServiceContract]
    interface ITinyTranslatorStatisticsService
    {

        [OperationContract]
        TranslationStatistics PerformTranslationStatistics(TranslationSelection translations);

    }

    public class TranslationStatistics
    {
        public Dictionary<TranslationStatus, int> StatusHistogram { get; set; }
        public Dictionary<String, int> LocaleCounts { get; set; }
        public int TotalCount { get; set; }
    }

}
