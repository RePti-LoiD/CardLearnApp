using CardLearnApp.Pages;
using Windows.UI.Xaml.Controls;

namespace CardLearnApp.DataPresentation.Cards
{
    public sealed partial class MiniCardElement : UserControl
    {
        private CardDataContainer container;
        public CardDataContainer Container { get => container; set => container = value; }

        private int elementIndex;
        public int ElementIndex { get => elementIndex; set => elementIndex = value; }

        public MiniCardElement()
        {
            InitializeComponent();
        }

        public MiniCardElement(CardDataContainer container)
        {
            this.container = container;
            InitializeComponent();
        }

        public MiniCardElement(CardDataContainer container, int elementIndex) : this(container)
        {
            this.elementIndex = elementIndex;
        }
    }
}