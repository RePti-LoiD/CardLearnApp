using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Windows.ApplicationModel.DataTransfer;

namespace CardLearnApp.Data
{
    public class MainDataSaveHandler
    {
        private static string directory = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
        public static readonly string JsonDataPath = Path.Combine(directory, "Bundles.json");

        public static void RestoreData()
        {
            MainDataContainer mainDataContainer = MainDataContainer.Initialize();
            if (File.Exists(JsonDataPath)) 
                mainDataContainer.Bundles = JsonConvert.DeserializeObject<MainDataContainer>(File.ReadAllText(JsonDataPath)).Bundles;
            else
                mainDataContainer.Bundles = new List<BundleContainer>();
        }

        public static void SaveAllData()
        {
            MainDataContainer mainDataContainer = MainDataContainer.Initialize();

            File.WriteAllText(JsonDataPath, JsonConvert.SerializeObject(mainDataContainer, new JsonSerializerSettings() { Formatting = Formatting.Indented }));

            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(JsonDataPath);

            Clipboard.SetContent(dataPackage);
        }
    }
}