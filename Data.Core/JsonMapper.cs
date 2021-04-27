using Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Data.Core
{
    public class JsonMapper : IJsonMapper
    {
        // it's no entity framework, just saving / loading collection from json file. Not thoroughly unit tested as likely replaced with better solution
        /// <summary>Gets the collection from json.</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List&lt;T&gt;.</returns>
        public List<T> GetCollectionFromJson<T>() where T : Model
        {
            var path = System.Reflection.Assembly.GetAssembly(typeof(JsonMapper)).Location;
            var jsonPath = File.ReadAllText(Path.Combine(Path.GetDirectoryName(path), typeof(T).Name + ".json"));
            return JsonConvert.DeserializeObject<List<T>>(jsonPath);
        }

        /// <summary>Saves the collection to json.</summary>
        /// <typeparam name="T"></typeparam>
        public void SaveCollectionToJson<T>(List<T> collection) where T : Model
        {
            var path = System.Reflection.Assembly.GetAssembly(typeof(JsonMapper)).Location;
            var jsonPath = Path.Combine(Path.GetDirectoryName(path), typeof(T).Name + ".json");
            var json = JsonConvert.SerializeObject(collection, Formatting.Indented);
            File.WriteAllText(jsonPath, json);
        }
    }
}
