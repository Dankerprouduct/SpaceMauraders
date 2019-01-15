using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace SpaceMarauders.Utilities
{
    public class SaveGame<T>
    {
        private string path; 
        private const string folderPath = @"Saves\";
        public SaveGame(string path)
        {
            this.path = path;
        }

        public void SaveGameData(T[] entities, string fileName)
        {
            
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string json = JsonConvert.SerializeObject(entities, Formatting.Indented, settings);
            string newPath = folderPath + @"\" + fileName + ".json";
            (new FileInfo(newPath)).Directory.Create();

            File.WriteAllText(newPath, json);


        }

    }
}
