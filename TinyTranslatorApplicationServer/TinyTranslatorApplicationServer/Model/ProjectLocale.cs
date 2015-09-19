using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TinyTranslatorApplicationServer.Model
{
    public class ProjectLocale
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public String LocaleCode { get; set; }

        [IgnoreDataMember]
        // diese nicht serialisieren wg. zirkulärer Referenz
        public Project Project { get; set; }
    }
}