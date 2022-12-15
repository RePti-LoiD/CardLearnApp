using CardLearnApp.Pages;
using System.Collections.Generic;

namespace CardLearnApp.Data
{
    [System.Serializable]
    public class CardsBundleContainer
    {
        private string bundleName = "Empty";
        public string BundleName { get => bundleName; set => bundleName = value; }
        

        private string bundleDescription = "Empty";
        public string BundleDescription { get => bundleDescription; set => bundleDescription = value; }


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