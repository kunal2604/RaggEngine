using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace RagengineEditor.Utilities
{
    public static class Serializer
    {
        public static void ToFile<T>(T instance, string path)
        {
            try
            {
                var settings = new XmlWriterSettings
                {
                    Indent = true,                  // Enable indentation
                    IndentChars = "\t",             // Use two spaces (you can change this to "\t" for tabs)
                    NewLineOnAttributes = false     // Optional: puts attributes on same line
                };

                using var fs = new FileStream(path, FileMode.Create);
                using var writer = XmlWriter.Create(fs, settings);

                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(writer, instance);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // TODO: log
            }
        }

        internal static T FromFile<T>(string path)
        {
            try
            {
                using var fs = new FileStream(path, FileMode.Open);
                var serializer = new DataContractSerializer(typeof(T));
                T instance = (T)serializer.ReadObject(fs);
                return instance;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // TO DO: log
                return default(T);
            }
        }
    }
}
