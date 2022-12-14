using CardLearnApp.Pages;
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

            mainDataContainer.Bundles = JsonConvert.DeserializeObject<MainDataContainer>(File.ReadAllText(JsonDataPath)).Bundles;
        }

        public static void SaveAllData()
        {
            List<CardsBundleContainer> bundles = new List<CardsBundleContainer>();
            List<CardDataContainer> cardDataContainers = new List<CardDataContainer>();

            cardDataContainers.Add(new CardDataContainer("Нападение Германии на СССР", "22 июня 1941"));
            cardDataContainers.Add(new CardDataContainer("Образование Ставки Главного Командования во главе со Сталиным", "23 июня"));
            cardDataContainers.Add(new CardDataContainer("Создание Совета по эвакуации", "24 июня"));
            cardDataContainers.Add(new CardDataContainer("Директива ЦК о мобилизации всех сил и средств на отпор врагу", "29 июня"));
            cardDataContainers.Add(new CardDataContainer("Был создан Государственный комитет обороны", "30 июня 1941"));
            cardDataContainers.Add(new CardDataContainer("Мощное наступление немецко-фашистских войск и оккупация Прибалтики, Белоруссии, Украины", "Июль- ноябрь 1941"));
            cardDataContainers.Add(new CardDataContainer("Смоленское сражение", "10 июля - 10 сентября 1941"));
            cardDataContainers.Add(new CardDataContainer("Оборона Киева", "11 июля - 19 сентября"));
            cardDataContainers.Add(new CardDataContainer("Оборона Одессы", "5 августа - 16 октября"));
            cardDataContainers.Add(new CardDataContainer("Приказ наркома обороны СССР №270.", "16 августа"));
            cardDataContainers.Add(new CardDataContainer("Поражение германских войск в районе Ельни", "30 августа - 6 сентября"));
            cardDataContainers.Add(new CardDataContainer("Начало блокады Ленинграда", "8 сентября"));
            cardDataContainers.Add(new CardDataContainer("Создание Советской гвардии", "18 сентября"));
            cardDataContainers.Add(new CardDataContainer("Начало Московского сражения", "30 сентября"));
            cardDataContainers.Add(new CardDataContainer("Начало обороны Севастополя (250 дней)", "30 октября"));
            cardDataContainers.Add(new CardDataContainer("Начало 2-го наступления немецких войск на Москву", "15 ноября"));
            cardDataContainers.Add(new CardDataContainer("Начало контрнаступление советских войск под Москвой", "5-6 декабря"));
            cardDataContainers.Add(new CardDataContainer("Освобождение территории Московской области от немецких войск", "Январь 1942 года"));
            cardDataContainers.Add(new CardDataContainer("Поражение советских войск на юге страны", "Весна - осень 1942"));
            cardDataContainers.Add(new CardDataContainer("Оборонительный этап Сталинградской битвы", "17 июля - 18 ноября 1942"));

            bundles.Add(new CardsBundleContainer(cardDataContainers) { BundleName = "Тест История" });
            bundles.Add(new CardsBundleContainer(new List<CardDataContainer>()) { BundleName = "Тест История(Пустой)" });
            bundles.Add(new CardsBundleContainer(cardDataContainers) { BundleName = "Тест История(2)" });

            MainDataContainer mainDataContainer = MainDataContainer.Initialize();
            mainDataContainer.Bundles = bundles;

            File.WriteAllText(JsonDataPath, JsonConvert.SerializeObject(mainDataContainer, new JsonSerializerSettings() { Formatting = Formatting.Indented }));

            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(JsonDataPath);

            Clipboard.SetContent(dataPackage);
        }
    }
}