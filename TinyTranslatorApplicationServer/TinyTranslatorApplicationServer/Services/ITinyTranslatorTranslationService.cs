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

        /* TODO
        /// <summary>
        /// Gets all projects.
        /// </summary>
        [OperationContract]
        ICollection<Project> GetProjects();

        /// <summary>
        /// Gets all assemblies of the project.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [OperationContract]
        ICollection<ResourceAssembly> GetAssemblies(int projectID);

        [OperationContract]
        ICollection<ResourceBundle> GetAllBundles(int projectID);

        [OperationContract]
        ICollection<ResourceBundle> GetAssemblyBundles(int projectID, int assemblyID);
        */

        /// <summary>
        /// Gets a resource bundle with all resources and translations.
        /// </summary>
        [OperationContract]
        ResourceBundle GetResourceBundle(int bundleID);

        // TODO CRUD für Translations etc.

    }
}
