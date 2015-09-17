using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TinyTranslatorApplicationServer.Model
{
    [TrackChanges]
    public class ResourceTranslation
    {
        public int ID { get; set; }
        
        public int ProjectID { get; set; }
        public int ResourceAssemblyID { get; set; }
        public int ResourceBundleID { get; set; }
        public int ResourceID { get; set; }
        public Resource Resource { get; set; }

        public String Locale { get; set; }
        
        public String StringValue { get; set; }
        public byte[] BinaryValue { get; set; }
        
        public TranslationStatus TranslationStatus { get; set; }
        public String TranslationBy { get; set; }
        public DateTime TranslationDateTime { get; set; }

    }

}