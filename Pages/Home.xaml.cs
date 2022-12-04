using CardLearnApp.Data;
using System.Collections.Generic;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CardLearnApp.Pages
{
    public sealed partial class Home : Page
    {
        private List<CardsBundleElement> cardsArrayContainers = new List<CardsBundleElement>();

        public Home()
        {
            InitializeComponent();

            BasicGridView.Loaded += (x, y) =>
            {
                List<CardDataContainer> cardDataContainers = new List<CardDataContainer>();

                cardDataContainers.Add(new CardDataContainer("Оборона Киева", "11 июля - 19 сентября"));
                cardDataContainers.Add(new CardDataContainer("Приказ наркома обороны СССР №270.", "16 августа"));
                cardDataContainers.Add(new CardDataContainer("Начало блокады Ленинграда", "8 сентября"));
                cardDataContainers.Add(new CardDataContainer("Оборона Одессы", "5 августа - 16 октября"));

                cardsArrayContainers.Add(new CardsBundleElement(new CardsBundleContainer(cardDataContainers) { BundleName = "Тест История" }));
                cardsArrayContainers.Add(new CardsBundleElement(new CardsBundleContainer(new List<CardDataContainer>()) { BundleName = "Тест История(Пустой)" }));
                cardsArrayContainers.Add(new CardsBundleElement(new CardsBundleContainer(cardDataContainers) { BundleName = "Тест История(2)" }));

                BasicGridView.ItemsSource = cardsArrayContainers;
            };

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ConnectedAnimation anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(Icon);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", Icon);
        }

        private void ItemClicked(object sender, ItemClickEventArgs e)
        {
            (e.ClickedItem as CardsBundleElement).OnCardClick();

            MainPage.Instance.NavigateFrame("Card", (e.ClickedItem as CardsBundleElement).Container);
        }
    }
}