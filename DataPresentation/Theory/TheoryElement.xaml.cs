using CardLearnApp.Data.TheoryData;
using System;
using Windows.UI.Xaml.Controls;

namespace CardLearnApp.DataPresentation.Theory
{
    public sealed partial class TheoryElement : UserControl
    {
        private TheoryDataContainer dataContainer;
        public TheoryDataContainer DataContainer { get => dataContainer; set { dataContainer = value; SetPicture(value.LinkToPic); } }

        private void SetPicture(string url)
        {
            Preview.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(url));
        }

        public TheoryElement()
        {
            InitializeComponent();
        }
    }
}