using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Model
{
    public class SettingsModel
    { 
        private static readonly string PATH = $"{Environment.CurrentDirectory}\\settings.json";
        private static SettingsModel instance;
        private int _lastId;
        private int _wodsAmount;
        public int LastId
        {
            get
            {
                return _lastId;
            }
            set
            {
                _lastId = value;
                UpdateJson();
            }
        }
        public int WordsAmount
        {
            get
            {
                return _wodsAmount;
            }
            set
            {
                _wodsAmount = value;
                UpdateJson();
                
            }
        }

        private SettingsModel()
        { 

        }

        // I use there a singleton pattern. 
        // In each part of program I will have the same settings.
        public static SettingsModel GetInstance()
        {
            if (instance == null)
            {
                instance = LoadData();
            }

            return instance;
        }
        
        // Method to load and store data in json file 
        private static SettingsModel LoadData()
        {
            if (!File.Exists(PATH))
            {
                File.CreateText(PATH).Dispose();
                return new SettingsModel();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<SettingsModel>(fileText);
            }
        }

        private static void SaveData(SettingsModel settings)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(settings);
                writer.Write(output);
            }

        }

        private void UpdateJson()
        {
            if (instance != null)
            {
                SaveData(instance);
            }
        }

    }
}
