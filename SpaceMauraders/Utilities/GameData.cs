using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Newtonsoft.Json;

namespace SpaceMauraders.Utilities
{
    public class GameData<T>
    {

        private JsonSerializerSettings settings;
        private const string folderPath = @"Saves\";

        /// <summary>
        /// give the name of the subfolder inside of "Saves"
        /// Create a new one if needed
        /// </summary>
        /// <param name="path"></param>
        public  GameData()
        {
            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None
            };
            
        }

        /// <summary>
        /// Serializes Data into an indented json file
        /// </summary>
        /// <param name="data">data to serialize</param>
        /// <param name="fileName">name of file to create or overide</param>
        public void SaveData(T[] data, string fileName)
        {
            
            string json = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
            string newPath = folderPath + @"\" + fileName + ".json";
            (new FileInfo(newPath)).Directory.Create();

            File.WriteAllText(newPath, json);
        }

        /// <summary>
        /// Deserializes data into an array
        /// </summary>
        /// <param name="fileName">name of file to deserialize</param>
        /// <returns></returns>
        public T[] LoadData(string fileName)
        {
            string newPath = folderPath + @"\" + fileName + ".json";

            string contents = File.ReadAllText(newPath);
            T[] tempEntities = JsonConvert.DeserializeObject<T[]>(contents, settings);
            for (int i = 0; i < tempEntities.Length; i++)
            {
                //Console.WriteLine(tempEntities[i].GetType().ToString());
            }
            return tempEntities;

        }


    }
}
