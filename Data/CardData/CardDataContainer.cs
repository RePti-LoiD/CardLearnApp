using System;
using Windows.UI.Xaml.Media;

namespace CardLearnApp.Pages
{
    public class CardDataContainer
    {
        private string frontSideText;
        private string backSideText;

        private bool isLearned;

        public string FrontSideText
        {
            get { return frontSideText; }
            set { frontSideText = value; }
        }

        public string BackSideText
        {
            get { return backSideText; }
            set { backSideText = value; }
        }

        public bool IsLearned
        {
            get { return isLearned; }
            set { isLearned = value; }
        }

        [NonSerialized]
        public PlaneProjection planeProjection;

        public CardDataContainer() { }

        public CardDataContainer(string frontSide, string backSide)
        {
            frontSideText = frontSide;
            backSideText = backSide;
        }
    }
}