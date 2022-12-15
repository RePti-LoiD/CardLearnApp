using Newtonsoft.Json;
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

            mainDataContainer.Bundles = JsonConvert.DeserializeObject<MainDataContainer>(File.ReadAllText(JsonDataPath)).Bundles;
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