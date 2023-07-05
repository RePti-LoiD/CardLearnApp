using CardLearnApp.Pages;
using Windows.UI.Xaml.Controls;

namespace CardLearnApp.DataPresentation
{
    public sealed partial class CardEditorNode : UserControl
    {
        public delegate void RemoveSelf(CardEditorNode cardEditorNode);
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

        private CardDataContainer dataContainer;

        public CardDataContainer DataContainer { get => dataContainer; set => dataContainer = value; }

        public CardEditorNode()
        {
            InitializeComponent();
        }

        private void FrontSideTextChanged(object sender, TextChangedEventArgs e)
        {
            dataContainer.FrontSideText = FrontSide.Text;
        }

        private void BackSideTextChanged(object sender, TextChangedEventArgs e)
        {
            dataContainer.BackSideText = BackSide.Text;
        }

        private void RemoveSelfClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            onRemoveSelf?.Invoke(this);
        }
    }
}