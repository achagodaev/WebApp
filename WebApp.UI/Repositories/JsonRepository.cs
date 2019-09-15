using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApp.UI.Repositories
{
    public class JsonRepository
    {
        private static JsonSerializer serializer;

        static JsonRepository()
        {
            serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
        }

        public static T Deserialize<T>(string path)
        {
            using (StreamReader streamReader = File.OpenText(path))
            {
                return (T)serializer.Deserialize(streamReader, typeof(T));
            }
        }

        public static async Task<T> DeserializeAsync<T>(string path)
        {
            return await Task.Run(() => Deserialize<T>(path));
        }

        public static void Serialize<T>(T entity, string path)
        {
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