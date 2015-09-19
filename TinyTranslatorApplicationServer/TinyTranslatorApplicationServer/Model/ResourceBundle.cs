using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TinyTranslatorApplicationServer.Model
{
    public class ResourceBundle
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int ProjectID { get; set; }
        public int ResourceAssemblyID { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime LastChangeDateTime { get; set; }
        public BundleSyncStatus BundleSyncStatus { get; set; }
        public TranslationStatus WorstTranslationStatus { get; set; } // wird anhand Translations und Resources berechnet

        public ResourceAssembly ResourceAssembly { get; set; }
        public ICollection<Resource> Resources { get; set; }
    }

    public enum BundleSyncStatus
    {
        ADDED,
        UPDATED,
        REMOVED
    }
}