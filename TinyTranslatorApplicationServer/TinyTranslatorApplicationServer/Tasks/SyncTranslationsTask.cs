using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.DAL;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Tasks
{
    public class SyncTranslationsTask
    {

        private ProjectRepository projectRepository;
        private ResourceAssemblyRepository assemblyRepository;
        private ResourceBundleRepository bundleRepository;
        private ResourceRepository resourceRepository;
        private ResourceTranslationRepository translationRepository;

        private TranslationSyncStatistics statistics = new TranslationSyncStatistics();

        public SyncTranslationsTask(ProjectRepository projectRepository, ResourceAssemblyRepository assemblyRepository, ResourceBundleRepository bundleRepository, 
            ResourceRepository resourceRepository, ResourceTranslationRepository translationRepository)
        {
            this.projectRepository = projectRepository;
            this.assemblyRepository = assemblyRepository;
            this.bundleRepository = bundleRepository;
            this.resourceRepository = resourceRepository;
            this.translationRepository = translationRepository;
        }

        public TranslationSyncStatistics Statistics { get { return statistics; } }

        public ResourceAssembly SyncTranslations(ResourceAssembly assembly, ResourceBundle bundle, List<ResourceTranslation> translations)
        {
            Project project = projectRepository.GetProjectByID(assembly.ProjectID);
            if (project == null)
                throw new ArgumentException("Project ID not found: {0}", assembly.ProjectID.ToString());

            ResourceAssembly existingAssembly = assemblyRepository.FindAssemblyByName(assembly.ProjectID, assembly.FileName);
            if (existingAssembly == null)
                throw new ArgumentException("Assembly not found: {0}", assembly.FileName);

            ResourceBundle existingBundle = bundleRepository.FindBundleWithResourcesAndTranslationsByName(assembly.ProjectID, existingAssembly.ID, bundle.Name);
            if (existingBundle == null)
                throw new ArgumentException("Bundle not found: {0}", bundle.Name);

            foreach (var translation in translations)
            {
                // also syncs resource translation status!
                SyncTranslation(project, existingAssembly, existingBundle, translation);
            }

            new TranslationStatusUtil().CalcBundleTranslationStatusFromResources(existingBundle);
            // Assembly status done by caller via CalculateAssemblyStatusFromBundles

            return existingAssembly;
        }

        private void SyncTranslation(Project project, ResourceAssembly existingAssembly, ResourceBundle existingBundle, ResourceTranslation translation)
        {
            // try find resource
            var existingResource = existingBundle.Resources.First(r => r.Key == translation.Resource.Key);
            if (existingResource == null)
            {
                Statistics.ResourceNotFound++;
                return;
            }

            // compare resource
            if (existingResource.ResourceClass != translation.Resource.ResourceClass)
            {
                Statistics.ResourceMismatch++;
                return;
            }

            if (existingResource.StringValue == translation.StringValue &&
                existingResource.BinaryValue.SequenceEqual(translation.BinaryValue))
            {
                Statistics.UnchangedResource++;
                return;
            }

            // try find translation
            var existingTranslation = existingResource.Translations.First(t => t.Locale == translation.Locale);
            if (existingTranslation != null &&
                (existingTranslation.StringValue == translation.StringValue &&
                existingTranslation.BinaryValue.SequenceEqual(translation.BinaryValue) ||
                existingTranslation.TranslationStatus >= translation.TranslationStatus))
            {
                Statistics.ExistingTranslations++;
                return;
            }

            // maybe delete old
            if (existingTranslation != null)
                translationRepository.DeleteTranslation(existingTranslation);

            // add new translation
            ResourceTranslation translationObj = CreateTranslation(existingAssembly, existingBundle, existingResource, translation);
            translationRepository.AddTranslation(translationObj);
            existingResource.Translations.Add(translationObj);
            Statistics.AddedTranslations++;

            // update resource translation status
            new TranslationStatusUtil().CalcResourceTranslationStatusFromTranslations(project.Translations, existingResource);
        }

        private ResourceTranslation CreateTranslation(ResourceAssembly existingAssembly, ResourceBundle existingBundle, Resource existingResource, ResourceTranslation template)
        {
            ResourceTranslation translation = new ResourceTranslation();
            translation.BinaryValue = template.BinaryValue;
            translation.Locale = template.Locale;
            translation.ProjectID = existingAssembly.ProjectID;
            translation.Resource = existingResource;
            translation.ResourceAssemblyID = existingAssembly.ID;
            translation.ResourceBundleID = existingBundle.ID;
            translation.ResourceID = existingResource.ID;
            translation.StringValue = template.StringValue;
            translation.TranslationBy = template.TranslationBy;
            translation.TranslationDateTime = template.TranslationDateTime;
            translation.TranslationStatus = template.TranslationStatus;
            return translation;
        }

        public void CalculateAssemblyStatusFromBundles(ResourceAssembly assembly)
        {
            assembly.WorstTranslationStatus = bundleRepository.GetWorstTranslationStatusForAssemblyFromBundles(assembly);
        }

    }
}