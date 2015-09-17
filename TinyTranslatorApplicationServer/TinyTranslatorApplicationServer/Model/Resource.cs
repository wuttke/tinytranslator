using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TinyTranslatorApplicationServer.Model
{
    [TrackChanges]
    public class Resource
    {
        public int ID { get; set; }

        public int ProjectID { get; set; }
        public int ResourceBundleID { get; set; }
        public ResourceBundle ResourceBundle { get; set; }

        public String Key { get; set; }
        public String StringValue { get; set; }
        public byte[] BinaryValue { get; set; }
        public ResourceType ResourceType { get; set; }
        public String ResourceClass { get; set; }
        public bool DesignerSupportFlag { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime LastChangeDateTime { get; set; }
        public ResourceSyncStatus ResourceSyncStatus { get; set; }
        public TranslationStatus WorstTranslationStatus { get; set; } // wird anhand Translations berechnet

        public ICollection<ResourceTranslation> Translations { get; set; }

        public bool NeedsTranslation()
        {
            return !DesignerSupportFlag && (ResourceType == ResourceType.STRING || ResourceType == ResourceType.STRING_ARRAY) && ResourceSyncStatus != ResourceSyncStatus.REMOVED;
        }

    }

    public enum ResourceType
    {
        STRING,
        STRING_ARRAY,
        IMAGE,
        OTHER_BINARY
    }

    public enum ResourceSyncStatus
    {
        ADDED,
        UPDATED,
        REMOVED
    }

}