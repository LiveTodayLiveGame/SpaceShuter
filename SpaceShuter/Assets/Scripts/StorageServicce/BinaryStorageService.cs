using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace LiveToday
{
    public class BinaryStorageService : IStorageService
    {
        public void Save(string key, object data, Action<bool> callBack = null)
        {
            var path = BuildPath(key);

            using (var fileStream = File.Create(path))
                new BinaryFormatter().Serialize(fileStream, data);
            
            callBack?.Invoke(true);
        }

        public void Load<T>(string key, Action<T> callBack) 
        {
            var path = BuildPath(key);

            if (File.Exists(path))
            {
                using var fileStream = File.Open(path, FileMode.Open);
                var loadedData = new BinaryFormatter().Deserialize(fileStream);

                callBack.Invoke((T)loadedData);
            }
            else
                callBack.Invoke(default(T));
        }
            

        private string BuildPath(string key)
        {
            const string directoryName = "Saves";
            var directoryPath = Path.Combine(Application.persistentDataPath + "/" + directoryName);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var filePath = directoryPath + "/" + key + ".dat";

            return filePath;
        }
    }
}