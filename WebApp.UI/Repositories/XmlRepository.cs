using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace WebApp.UI.Repositories
{
    public class XmlRepository
    {
        public static T Deserialize<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (StreamReader streamReader = File.OpenText(path))
            {
                return (T)serializer.Deserialize(streamReader);
            }
        }

        public static async Task<T> DeserializeAsync<T>(string path)
        {
            return await Task.Run(() => Deserialize<T>(path));
        }

        public static void Serialize<T>(T entity, string path)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (StreamWriter streamWriter = File.CreateText(path))
            {
                serializer.Serialize(streamWriter, entity);
            }
        }

        public static async Task SerializeAsync<T>(T entity, string path)
        {
            await Task.Run(() => Serialize(entity, path));
        }
    }
}