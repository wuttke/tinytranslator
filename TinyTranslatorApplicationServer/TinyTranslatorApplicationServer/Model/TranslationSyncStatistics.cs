using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinyTranslatorApplicationServer.Model
{
    public class TranslationSyncStatistics
    {
        
        /// <summary>
        /// Translations that have been added
        /// </summary>
        public int AddedTranslations { get; set; }

        /// <summary>
        /// Translations which already exist (with a higher or equal status)
        /// </summary>
        public int ExistingTranslations { get; set; }

        /// <summary>
        /// Translations without matching resource
        /// </summary>
        public int ResourceNotFound { get; set; }

        /// <summary>
        /// Translations that do not fit the resource type
        /// </summary>
        public int ResourceMismatch { get; set; }

        /// <summary>
        /// Translations which do not change the original value of the resource 
        /// </summary>
        public int UnchangedResource { get; set; }

    }
}