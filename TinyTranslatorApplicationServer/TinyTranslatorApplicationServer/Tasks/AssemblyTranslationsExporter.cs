using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Web;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Tasks
{
    public class AssemblyTranslationsExporter : ITranslationExporter
    {

        private String outputDir;
        private String assemblyName;
        private String locale;
        private ModuleBuilder moduleBuilder;
        private AssemblyBuilder assemblyBuilder;

        public AssemblyTranslationsExporter(String outputDir, String assemblyName, String locale)
        {
            this.outputDir = outputDir;
            this.assemblyName = assemblyName;
            this.locale = locale;

            CreateAssemblyBuilder();
        }

        private void CreateAssemblyBuilder()
        {
            var assName = new AssemblyName();
            assName.CultureInfo = new CultureInfo(locale);
            assName.Name = assemblyName + ".resources"; // XXX.resources.dll
            assName.CodeBase = outputDir;

            var domain = Thread.GetDomain();
            assemblyBuilder = domain.DefineDynamicAssembly(assName, AssemblyBuilderAccess.Save, outputDir);

            String fileName = assName.Name + ".dll";
            moduleBuilder = assemblyBuilder.DefineDynamicModule(fileName, fileName);
        }

        public void ExportTranslationsForBundle(ResourceBundle bundle, String locale, List<ResourceTranslation> translations)
        {
            String resourceName = bundle.Name + "." + locale + ".resources";
            var writer = moduleBuilder.DefineResource(resourceName, "", ResourceAttributes.Public);
            foreach (var translation in translations)
            {
                Debug.Assert(translation.Locale == locale);
                Debug.Assert(bundle.ID == translation.ResourceBundleID);
                if (translation.Resource.ResourceType == ResourceType.STRING || translation.Resource.ResourceType == ResourceType.STRING_ARRAY) //?
                    writer.AddResource(translation.Resource.Key, translation.StringValue);
                else if (translation.Resource.ResourceType == ResourceType.IMAGE)
                    writer.AddResource(translation.Resource.Key, Image.FromStream(new MemoryStream(translation.BinaryValue)));
                else
                {
                    Object obj = ConvertToObject(translation.BinaryValue, translation.Resource.ResourceClass);
                    writer.AddResource(translation.Resource.Key, translation.BinaryValue);
                }
            }
        }

        private object ConvertToObject(byte[] data, string resourceType)
        {
            MemoryStream dataStream = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(dataStream);

            if (resourceType.Equals("ResourceTypeCode.Null")) 
                return null;
            else if (resourceType.Equals("ResourceTypeCode.String"))
                return reader.ReadString();
            else if (resourceType.Equals("ResourceTypeCode.Boolean")) 
                return reader.ReadBoolean();
            else if (resourceType.Equals("ResourceTypeCode.Char")) 
                return (char) reader.ReadUInt16();
            else if (resourceType.Equals("ResourceTypeCode.Byte"))
                return reader.ReadByte();
            else if (resourceType.Equals("ResourceTypeCode.SByte")) 
                return reader.ReadSByte();
            else if (resourceType.Equals("ResourceTypeCode.Int16")) 
                return reader.ReadInt16();
            else if (resourceType.Equals("ResourceTypeCode.UInt16"))
                return reader.ReadUInt16();
            else if (resourceType.Equals("ResourceTypeCode.Int32")) 
                return reader.ReadInt32();
            else if (resourceType.Equals("ResourceTypeCode.UInt32")) 
                return reader.ReadUInt32();
            else if (resourceType.Equals("ResourceTypeCode.Int64"))
                return reader.ReadInt64();
            else if (resourceType.Equals("ResourceTypeCode.UInt64")) 
                return reader.ReadUInt64();
            else if (resourceType.Equals("ResourceTypeCode.Single")) 
                return reader.ReadSingle();
            else if (resourceType.Equals("ResourceTypeCode.Double"))
                return reader.ReadDouble();
            else if (resourceType.Equals("ResourceTypeCode.Decimal")) 
                return reader.ReadDecimal();
            else if (resourceType.Equals("ResourceTypeCode.DateTime")) 
            {
                // Use DateTime's ToBinary & FromBinary.
                Int64 dateData = reader.ReadInt64(); 
                return DateTime.FromBinary(dateData);
            }
            else if (resourceType.Equals("ResourceTypeCode.TimeSpan"))
            {
                Int64 ticks = reader.ReadInt64(); 
                return new TimeSpan(ticks);
            }
            // Special types 
            else if (resourceType.Equals("ResourceTypeCode.ByteArray"))
            { 
                int len = reader.ReadInt32();
                return reader.ReadBytes(len);
            }
            else if (resourceType.Equals("ResourceTypeCode.Stream"))
            {
                int len = reader.ReadInt32();
                return new MemoryStream(reader.ReadBytes(len));
            }
            else
            {
                // weitere Klassen
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(dataStream);
            }
        }

        public void FinishTranslationExport()
        {
            assemblyBuilder.Save(assemblyName + ".resources.dll");
        }

    }
}