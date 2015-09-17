//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace TinyTranslatorApplicationServer.Services
{
    public class TinyTranslatorStatisticsService : ITinyTranslatorStatisticsService
    {
        public TranslationStatistics PerformTranslationStatistics(TranslationSelection translations)
        {
            throw new NotImplementedException();
        }
    }
}
