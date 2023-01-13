using CardLearnApp.Pages;
using System;
using System.Collections.Generic;

namespace CardLearnApp.Data
{
    [Serializable]
    public class CardsBundleContainer
    {
        private string bundleName = "Empty";
        public string BundleName { get => bundleName; set => bundleName = value; }
        private DateTimeOffset deadLine = DateTime.Now;

        private string bundleDescription = "Empty";
        public string BundleDescription { get => bundleDescription; set => bundleDescription = value; }
        public DateTimeOffset DeadLine { get => deadLine; set => deadLine = value; }


        private List<CardDataContainer> cardDataContainers;
        
        public List<CardDataContainer> CardDataContainers
        {
            set { cardDataContainers = value; }
            get { return cardDataContainers; }
        }

        public void SetNewElementByIndex(int index, CardDataContainer cardDataContainer) =>
            cardDataContainers[index] = cardDataContainer;

        public CardDataContainer GetNewElementByIndex(int index) => 
            cardDataContainers[index];

        public void SetNewElement(CardDataContainer cardDataContainer) => 
            cardDataContainers.Add(cardDataContainer);

        public CardsBundleContainer() { }

        public CardsBundleContainer(List<CardDataContainer> cardDataContainers)
        {
            this.cardDataContainers = new List<CardDataContainer>();

            this.cardDataContainers = cardDataContainers;
        }
    }
}