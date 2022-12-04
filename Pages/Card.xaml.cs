using CardLearnApp.Data;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CardLearnApp.Pages
{
    public sealed partial class Card : Page
    {
        public CardsBundleContainer CardsBundleContainer { get; set; }

        public Card()
        {
            InitializeComponent();

            CardSlider.Loaded += (x, y) =>
            {
                
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ConnectedAnimation anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(CardSlider);
            }

            if (e.Parameter is CardsBundleElement element)
            {
                CardsBundleContainer = element.Container;

                List<CardElement> cardElements = new List<CardElement>();

                foreach (CardDataContainer item in CardsBundleContainer.CardDataContainers)
                    cardElements.Add(new CardElement() { Container = item });

                CardSlider.ItemsSource = cardElements;
                throw new System.SystemException();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", CardSlider);
        }
    }
}