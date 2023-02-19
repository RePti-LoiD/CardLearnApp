using CardLearnApp.Data.TheoryData;
using System;
using Windows.UI.Xaml.Controls;

namespace CardLearnApp.DataPresentation.Theory
{
    public sealed partial class TheoryEditorNode : UserControl
    {
        public delegate void RemoveSelf(TheoryEditorNode theoryEditorNode);
        private event RemoveSelf onRemoveSelf;

        public event RemoveSelf OnRemoveSelf
        {
            add
            {
                onRemoveSelf += value;
            }
            remove
            {
                onRemoveSelf -= value;
            }
        }

        private TheoryDataContainer dataContainer;
        public TheoryDataContainer DataContainer 
        { 
            get => dataContainer; 
            set 
            { 
                dataContainer = value;

                if (value.LinkToPic != null || value.LinkToPic != string.Empty) 
                    SetPicture(value.LinkToPic); 
            } 
        }

        public TheoryEditorNode()
        {
            InitializeComponent();
        }

        private void RemoveSelfClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            onRemoveSelf?.Invoke(this);
        }

        private void URLTextBoxLostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string url = (sender as TextBox).Text;
            if (url != null || url != string.Empty)
            {
                dataContainer.LinkToPic = url;
                SetPicture(url);
            }
        }

        private void SetPicture(string url)
        {
            Preview.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(url));
        }

        private void NameTextBoxLostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            dataContainer.TheoryBlockName = (sender as TextBox).Text;
        }

        private void DescriptionTextBoxLostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            dataContainer.TheoryBlockDescription = (sender as TextBox).Text;
        }
    }
}