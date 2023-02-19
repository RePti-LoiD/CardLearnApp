using CardLearnApp.Data.TheoryData;
using CardLearnApp.Pages;
using System;
using System.Collections.Generic;

namespace CardLearnApp.Data
{
    [Serializable]
    public class BundleContainer
    {
        private string bundleName = "Empty";
        public string BundleName { get => bundleName; set => bundleName = value; }
        private DateTimeOffset deadLine = DateTime.Now;

        private string bundleDescription = "Empty";
        public string BundleDescription { get => bundleDescription; set => bundleDescription = value; }
        public DateTimeOffset DeadLine { get => deadLine; set => deadLine = value; }


        private List<CardDataContainer> cardDataContainers;
        public List<CardDataContainer> CardDataContainers { set => cardDataContainers = value; get => cardDataContainers; }


        private List<TheoryDataContainer> theoryDataContainers;
        public List<TheoryDataContainer> TheoryDataContainers { get => theoryDataContainers; set => theoryDataContainers = value; }

        public BundleContainer() { }
    }
}