using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TinyTranslatorApplicationServer.Model
{
    public class ResourceAssembly
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public String FileName { get; set; }
        public String FileFormat { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime LastSyncDateTime { get; set; }
        public TranslationStatus WorstTranslationStatus { get; set; }
    }
}