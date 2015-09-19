using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Services
{
    [ServiceContract]
    public interface ITinyTranslatorTranslationService
    {

        [OperationContract]
        ResourceBundle GetResourceBundle(int bundleID);

        // TODO CRUD für Translations etc.

    }
}
