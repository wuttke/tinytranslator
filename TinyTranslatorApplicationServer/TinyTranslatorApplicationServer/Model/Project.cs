using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TinyTranslatorApplicationServer.Model
{
    [TrackChanges]
    public class Project
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String ProjectLocale { get; set; }
        public List<ProjectLocale> Translations { get; set; }
    }
}