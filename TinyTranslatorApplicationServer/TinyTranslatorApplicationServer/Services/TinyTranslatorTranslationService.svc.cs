using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TinyTranslatorApplicationServer.DAL;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Services
{
    public class TinyTranslatorTranslationService : ITinyTranslatorTranslationService
    {

        public ResourceBundle GetResourceBundle(int bundleID)
        {
            var context = new TinyTranslatorDbContext();
            return new ResourceBundleRepository(context).GetResourceBundle(bundleID);
        }

    }
}
