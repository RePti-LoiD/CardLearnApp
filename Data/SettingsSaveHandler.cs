using System.IO;

namespace CardLearnApp.Data
{
    internal class SettingsSaveHandler
    {
        private static string directory = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
        public static string JsonDataPath = Path.Combine(directory, "Bundles.json");


    }
}