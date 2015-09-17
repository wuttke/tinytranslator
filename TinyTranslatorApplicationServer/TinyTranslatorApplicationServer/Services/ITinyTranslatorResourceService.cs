using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TinyTranslatorApplicationServer.Model;
using TinyTranslatorApplicationServer.Sync;

namespace TinyTranslatorApplicationServer.Services
{

    [ServiceContract]
    public interface ITinyTranslatorResourceService
    {

        [OperationContract]
        ICollection<Project> GetProjects();

        [OperationContract]
        ResourceBundle GetResourceBundle(int bundleID);
        
        [OperationContract]
        SyncStatistics SyncResourceAssembly(ResourceAssembly ra, ICollection<ResourceBundle> bundles);

    }

}
