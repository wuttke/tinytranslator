﻿using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TinyTranslatorSyncAssemblyClient.TinyTranslatorApplicationServer;

namespace TinyTranslatorSyncAssemblyClient
{
    public class AssemblyBundleCollector
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Assembly ass;

        public AssemblyBundleCollector(Assembly assembly)
        {
            this.ass = assembly;
        }

        public void CollectAssembly(ResourceAssembly assembly)
        {
            assembly.FileFormat = ".NET Assembly";
            assembly.FileName = ass.GetName().Name;
            assembly.ProjectID = 1; // TODO
            logger.Info("Found assembly {0}", ass.FullName);
        }

        public void CollectBundles(List<ResourceBundle> bundles)
        {
            var resourceNames = ass.GetManifestResourceNames();
            foreach (var resourceName in resourceNames)
            {
                if (resourceName.EndsWith(".resources"))
                {
                    ResourceBundle bundle = CollectBundle(resourceName);
                    bundles.Add(bundle);
                    logger.Info("Collected {1} resources for bundle {0}", resourceName, bundle.Resources.Count);
                }
                else
                    logger.Info("Skipped manifest resource {0}", resourceName);
            }
        }

        private ResourceBundle CollectBundle(string resourceName)
        {
            var bundle = new ResourceBundle();
            bundle.CreateDateTime = DateTime.UtcNow;
            bundle.LastChangeDateTime = DateTime.UtcNow;
            bundle.Name = resourceName;
            bundle.Resources = new List<Resource>();

            CollectResourcesForBundle(resourceName, bundle);
            return bundle;
        }

        private void CollectResourcesForBundle(string resourceName, ResourceBundle bundle)
        {
            var stream = ass.GetManifestResourceStream(resourceName);
            var reader = new ResourceReader(stream);
            IDictionaryEnumerator dict = reader.GetEnumerator();
            while (dict.MoveNext())
                CollectResourcesFromResource(resourceName, dict, bundle, reader);
        }

        private void CollectResourcesFromResource(string resourceName, IDictionaryEnumerator dict, ResourceBundle bundle, ResourceReader reader)
        {
            Object dictValue = null;
            try
            {
                dictValue = dict.Value;
            }
            catch (Exception e)
            {
                // wenn eine Assembly wie MeonaKernel gebraucht wird, um dict.Value aufzulösen
            }
           
            CollectSingleResource(resourceName, dict, bundle, reader, dictValue);

            if (dictValue is String[] && dict.Key.ToString().StartsWith(">>"))
                CollectResourcesFromArray(resourceName, dict, bundle);
        }

        private void CollectSingleResource(string resourceName, IDictionaryEnumerator dict, ResourceBundle bundle, ResourceReader reader, Object dictValue)
        {
            var resource = new Resource();
            resource.Key = dict.Key.ToString();
            resource.CreateDateTime = DateTime.UtcNow;
            resource.LastChangeDateTime = DateTime.UtcNow;
            resource.DesignerSupportFlag = resource.Key.StartsWith(">>");

            String type;
            byte[] data;
            reader.GetResourceData(dict.Key.ToString(), out type, out data);
            resource.ResourceClass = type;
            resource.BinaryValue = data;

            bundle.Resources.Add(resource);

            resource.StringValue = dictValue != null ? dictValue.ToString() : null;
            if (dictValue is String)
                resource.ResourceType = ResourceType.STRING;
            else if (dictValue is Bitmap)
                resource.ResourceType = ResourceType.IMAGE;
            else
                resource.ResourceType = ResourceType.OTHER_BINARY;
        }

        private void CollectResourcesFromArray(string resourceName, IDictionaryEnumerator dict, ResourceBundle bundle)
        {
            String[] strings = (String[])dict.Value;
            for (int i = 0; i < strings.Length; i++)
            {
                var resource = new Resource();
                resource.Key = dict.Key.ToString() + "[" + i + "]";
                resource.StringValue = strings[i];
                resource.CreateDateTime = DateTime.UtcNow;
                resource.LastChangeDateTime = DateTime.UtcNow;
                
                resource.ResourceClass = "ResourceTypeCode.String";
                resource.BinaryValue = Encoding.UTF8.GetBytes(strings[i]);
                resource.ResourceType = ResourceType.STRING_ARRAY;
                
                bundle.Resources.Add(resource);
            }
        }

    }
}
