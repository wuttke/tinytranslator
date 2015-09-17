using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyTranslatorApplicationServer.Sync
{
    public class SyncStatistics
    {
        public int AddedAssemblies { get; set; }

        public int AddedBundles { get; set; }
        public int UpdatedBundles { get; set; }
        public int RemovedBundles { get; set; }

        public int AddedResources { get; set; }
        public int UpdatedResources { get; set; }
        public int RemovedResources { get; set; }
    }
}
