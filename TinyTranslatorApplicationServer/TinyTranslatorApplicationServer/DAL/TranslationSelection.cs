using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinyTranslatorApplicationServer.DAL
{
    public class TranslationSelection
    {
        public int ProjectID { get; set; }
        public String Locale { get; set; }
        public int AssemblyID { get; set; }
        public int? BundleID { get; set; }
        public int? ResourceID { get; set; }
    }
}