using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace RagengineEditor.Utilities
{
    public static class Serializer
    {
        public static void ToFile<T>(T instance, string path)
        {
            try
            {
                using var fs = new FileStream(path, FileMode.Create);
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(fs, instance);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // TO DO: log
            }
        }
    }
}
