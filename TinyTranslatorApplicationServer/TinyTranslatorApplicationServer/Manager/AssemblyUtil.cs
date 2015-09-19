using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TinyTranslatorApplicationServer.Manager
{
    public class AssemblyUtil
    {

        public Assembly GetAssemblyFromStream(Stream assemblyStream)
        {
            String tempAssemblyPath = Path.GetTempFileName();
            
            var fileStream = File.Create(tempAssemblyPath);
            assemblyStream.CopyTo(fileStream);
            fileStream.Close();
            
            var assembly = Assembly.LoadFrom(tempAssemblyPath);
            
            //File.Delete(tempAssemblyPath);

            return assembly;
        }

    }
}