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
     
        [SkipTracking]
        public int ProjectID { get; set; }

        [SkipTracking]
        public int ResourceAssemblyID { get; set; }
        
        [SkipTracking]
        public int ResourceBundleID { get; set; }
        
        [SkipTracking]
        public int ResourceID { get; set; }

        [SkipTracking]
        public Resource Resource { get; set; }

        [SkipTracking]
        public String Locale { get; set; }
        
        public String StringValue { get; set; }
        public byte[] BinaryValue { get; set; }
        
        public TranslationStatus TranslationStatus { get; set; }
        public String TranslationBy { get; set; }
        public DateTime TranslationDateTime { get; set; }

    }

}