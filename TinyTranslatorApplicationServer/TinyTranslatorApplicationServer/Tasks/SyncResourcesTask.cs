﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.DAL;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Tasks
{
    public class SyncResourcesTask
    {

        private ResourceSyncStatistics syncStatistics = new ResourceSyncStatistics();

        private ResourceAssemblyRepository assemblyRepository;
        private ResourceBundleRepository bundleRepository;
        private ResourceRepository resourceRepository;
        private ResourceTranslationRepository translationRepository;

        public SyncResourcesTask(ResourceAssemblyRepository assemblyRepository, ResourceBundleRepository bundleRepository, ResourceRepository resourceRepository, ResourceTranslationRepository translationRepository)
        {
            this.assemblyRepository = assemblyRepository;
            this.bundleRepository = bundleRepository;
            this.resourceRepository = resourceRepository;
            this.translationRepository = translationRepository;
        }

        public ResourceSyncStatistics Statistics
        {
            get { return syncStatistics; }
        }

        public void SyncAllBundlesWithDeletions(ResourceAssembly existingAssembly, ICollection<ResourceBundle> newBundles)
        {
            var existingBundles = bundleRepository.FindBundlesForAssembly(existingAssembly);

            foreach (var newBundle in newBundles)
            {
                var existingBundle = existingBundles.FirstOrDefault(b => b.Name == newBundle.Name);
                if (existingBundle == null)
                    CreateNewBundle(existingAssembly, newBundle);
                else
                {
                    UpdateBundle(existingAssembly, existingBundle, newBundle);
                    existingBundles.Remove(existingBundle);
                }
            }

            foreach (var remainingExistingBundle in existingBundles)
            {
                MarkBundleDeleted(remainingExistingBundle);
            }
        }

        public void SyncSomeBundles(ResourceAssembly existingAssembly, ICollection<ResourceBundle> newBundles)
        {
            foreach (var newBundle in newBundles)
            {
                var existingBundle = bundleRepository.FindBundleByName(existingAssembly.ProjectID, existingAssembly.ID, newBundle.Name);
                if (existingBundle == null)
                    CreateNewBundle(existingAssembly, newBundle);
                else
                    UpdateBundle(existingAssembly, existingBundle, newBundle);
            }
        }

        private void MarkBundleDeleted(ResourceBundle bundle)
        {
            if (bundle.BundleSyncStatus != BundleSyncStatus.REMOVED)
            {
                syncStatistics.RemovedBundles++;

                bundle.BundleSyncStatus = BundleSyncStatus.REMOVED;
                bundle.LastChangeDateTime = DateTime.UtcNow;

                MarkResourcesDeletedForBundle(bundle);
            }
        }

        private void MarkResourcesDeletedForBundle(ResourceBundle bundle)
        {
            var resources = resourceRepository.GetResourcesForBundle(bundle);
            foreach (var resource in resources)
                MarkResourceDeleted(resource);
        }

        private void MarkResourceDeleted(Resource resourceToDelete)
        {
            if (resourceToDelete.ResourceSyncStatus != ResourceSyncStatus.REMOVED)
            {
                syncStatistics.RemovedResources++;

                resourceToDelete.ResourceSyncStatus = ResourceSyncStatus.REMOVED;
                resourceToDelete.LastChangeDateTime = DateTime.UtcNow;
            }
        }

        private void UpdateBundle(ResourceAssembly existingAssembly, ResourceBundle existingBundle, ResourceBundle newBundle)
        {
            bool haveChanges = UpdateResources(existingAssembly, existingBundle, newBundle.Resources);

            if (haveChanges)
            {
                syncStatistics.UpdatedBundles++;

                existingBundle.LastChangeDateTime = DateTime.UtcNow;
                existingBundle.BundleSyncStatus = BundleSyncStatus.UPDATED;
                new TranslationStatusUtil().CalcBundleTranslationStatusFromResources(existingBundle);
            }
        }

        private bool UpdateResources(ResourceAssembly existingAssembly, ResourceBundle existingBundle, ICollection<Resource> newResources)
        {
            bool changes = false;

            var existingResources = resourceRepository.GetResourcesForBundle(existingBundle);
            List<Resource> resourcesToDelete = new List<Resource>(existingResources);

            foreach (var newResource in newResources)
            {
                var existingResource = existingResources.FirstOrDefault(r => r.Key == newResource.Key);
                if (existingResource == null)
                {
                    CreateNewResource(existingAssembly, existingBundle, newResource);
                    changes = true;
                }
                else
                {
                    resourcesToDelete.Remove(existingResource);
                    if (HasResourceChanges(existingResource, newResource))
                    {
                        changes = true;
                        translationRepository.DeleteTranslationsForResource(existingResource);
                        ApplyChangesToResource(existingResource, newResource);
                    }
                }
            }

            foreach (var resourceToDelete in resourcesToDelete)
            {
                changes = true;
                MarkResourceDeleted(resourceToDelete);
            }

            return changes;
        }

        private void ApplyChangesToResource(Resource existingResource, Resource template)
        {
            existingResource.BinaryValue = template.BinaryValue;
            existingResource.DesignerSupportFlag = template.DesignerSupportFlag;
            existingResource.ResourceClass = template.ResourceClass;
            existingResource.ResourceType = template.ResourceType;
            existingResource.StringValue = template.StringValue;
            existingResource.WorstTranslationStatus = existingResource.NeedsTranslation() ? TranslationStatus.NOT_TRANSLATED : TranslationStatus.NO_NEED_TO_TRANSLATE;

            existingResource.ResourceSyncStatus = ResourceSyncStatus.UPDATED;
            existingResource.LastChangeDateTime = DateTime.UtcNow;

            syncStatistics.UpdatedResources++;
        }

        private bool HasResourceChanges(Resource existingResource, Resource template)
        {
            return (!existingResource.BinaryValue.SequenceEqual(template.BinaryValue)
                || existingResource.DesignerSupportFlag != template.DesignerSupportFlag
                || existingResource.ResourceClass != template.ResourceClass 
                || existingResource.ResourceType != template.ResourceType
                || existingResource.StringValue != template.StringValue);
        }

        private void CreateNewBundle(ResourceAssembly existingAssembly, ResourceBundle template)
        {
            var bundle = new ResourceBundle();
            bundle.BundleSyncStatus = BundleSyncStatus.ADDED;
            bundle.CreateDateTime = DateTime.UtcNow;
            bundle.LastChangeDateTime = DateTime.UtcNow;
            bundle.Name = template.Name;
            bundle.ProjectID = existingAssembly.ProjectID;
            bundle.ResourceAssembly = existingAssembly;
            bundle.Resources = new List<Resource>();
            bundleRepository.AddBundle(bundle);

            foreach (var newResource in template.Resources)
                CreateNewResource(existingAssembly, bundle, newResource);

            // es gibt noch keine Translations für dieses Bundle
            new TranslationStatusUtil().CalcBundleTranslationStatusFromResources(bundle);

            syncStatistics.AddedBundles++;
        }

        private void CreateNewResource(ResourceAssembly existingAssembly, ResourceBundle existingBundle, Resource template)
        {
            Resource resource = new Resource();
            resource.BinaryValue = template.BinaryValue;
            resource.CreateDateTime = DateTime.UtcNow;
            resource.DesignerSupportFlag = template.DesignerSupportFlag;
            resource.Key = template.Key;
            resource.LastChangeDateTime = DateTime.UtcNow;
            resource.ProjectID = existingAssembly.ProjectID;
            resource.ResourceBundle = existingBundle;
            resource.ResourceClass = template.ResourceClass;
            resource.ResourceSyncStatus = ResourceSyncStatus.ADDED;
            resource.ResourceType = template.ResourceType;
            resource.StringValue = template.StringValue;
            resource.WorstTranslationStatus = resource.NeedsTranslation() ? TranslationStatus.NOT_TRANSLATED : TranslationStatus.NO_NEED_TO_TRANSLATE;
            existingBundle.Resources.Add(resource);
            resourceRepository.AddResource(resource);

            syncStatistics.AddedResources++;
        }

        public ResourceAssembly SyncResourceAssembly(ResourceAssembly template)
        {
            var existingAssembly = assemblyRepository.FindAssemblyByName(template.ProjectID, template.FileName);
            if (existingAssembly == null)
            {
                existingAssembly = CreateNewAssembly(template);
            }
            else
            {
                existingAssembly.LastSyncDateTime = DateTime.UtcNow;
            }

            return existingAssembly;
        }

        private ResourceAssembly CreateNewAssembly(ResourceAssembly template)
        {
            ResourceAssembly newAssembly = new ResourceAssembly();
            newAssembly.CreateDateTime = DateTime.UtcNow;
            newAssembly.LastSyncDateTime = DateTime.UtcNow;
            newAssembly.FileFormat = template.FileFormat;
            newAssembly.FileName = template.FileName;
            newAssembly.ProjectID = template.ProjectID;
            assemblyRepository.AddAssembly(newAssembly);
            syncStatistics.AddedAssemblies++;
            return newAssembly;
        }

        public void CalculateAssemblyStatusFromBundles(ResourceAssembly assembly)
        {
            assembly.WorstTranslationStatus = bundleRepository.GetWorstTranslationStatusForAssemblyFromBundles(assembly);
        }

        public void DeleteNonExistingBundles(ResourceAssembly ra, List<string> existingBundleNames)
        {
            var bundleList = bundleRepository.FindBundlesForAssembly(ra);
            bundleList.RemoveAll(b => existingBundleNames.Contains(b.Name));
            foreach (var remainingExistingBundle in bundleList)
                MarkBundleDeleted(remainingExistingBundle);
        }

    }
}