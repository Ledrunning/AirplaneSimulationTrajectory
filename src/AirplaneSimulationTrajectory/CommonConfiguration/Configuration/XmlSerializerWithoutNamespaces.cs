using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CommonConfiguration.Configuration
{
    public static class XmlSerializerWithoutNamespaces
    {
        private static readonly XmlSerializerNamespaces emptyNs;

        static XmlSerializerWithoutNamespaces()
        {
            emptyNs = new XmlSerializerNamespaces();
            emptyNs.Add("", "");
        }

        public static void SaveAsXmlFile<T>(T @object, string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, @object, emptyNs);
            }
        }

        public static string Serialize<T>(T @object)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, @object, emptyNs);
                return textWriter.ToString();
            }
        }

        public static T DeserializeFromXmlFile<T>(string filePath)
            where T : new()
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = XmlReader.Create(filePath))
            {
                return (T) serializer.Deserialize(reader);
            }
        }
        
        public static T Deserialize<T>(string xmlData)
            where T : new()
        {
            var serializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(xmlData))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        public static T DeserializeFromStream<T>(string filePath)
        {
            using (var stream = new StreamReader(filePath))
            {
                var serializer = new XmlSerializer(typeof(T));
                var data = (T) serializer.Deserialize(stream);
                return data;
            }
        }
    }
}