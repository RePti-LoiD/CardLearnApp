using Windows.UI.Xaml.Media;

namespace CardLearnApp.Pages
{
    public class CardDataContainer
    {
        private string frontSideText;
        private string backSideText;

        private bool isFrontSide;

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

        public bool IsFrontSide
        {
            get { return isFrontSide;}
            set { isFrontSide = value;}
        }

        public PlaneProjection planeProjection;

        public CardDataContainer(string frontSide, string backSide)
        {
            frontSideText = frontSide;
            backSideText = backSide;
        }
    }
}