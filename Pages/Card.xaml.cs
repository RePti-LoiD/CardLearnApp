using CardLearnApp.Data;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CardLearnApp.Pages
{
    public sealed partial class Card : Page
    {
        private List<CardElement> cardElements;

        public CardsBundleContainer CardsBundleContainer { get; set; }
        public Card()
        {
            InitializeComponent();

            LearnButton.Click += (x, y) =>
            {
                cardElements[CardSlider.SelectedIndex].IsLearn = !cardElements[CardSlider.SelectedIndex].IsLearn;
            };

            CardSlider.Loaded += (x, y) =>
            {
                if (CardsBundleContainer != null)
                {
                    cardElements = new List<CardElement>();

                    foreach (CardDataContainer item in CardsBundleContainer.CardDataContainers)
                        cardElements.Add(new CardElement() { Container = item });

                    CardSlider.ItemsSource = cardElements;

                    BundleNameTxt.Text = CardsBundleContainer.BundleName;
                    BundleDesc.Text = CardsBundleContainer.BundleDescription;
                }
            };

            CardSlider.SelectionChanged += (x, y) =>
            {
                if (CardSlider.SelectedIndex < cardElements.Count && CardSlider.SelectedIndex >= 0)
                {
                    LearnButton.IsChecked = cardElements[CardSlider.SelectedIndex].IsLearn;

                    Progress.Value = CardSlider.SelectedIndex * 100 / (cardElements.Count - 1);
                }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ConnectedAnimation anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (anim != null)
                anim.TryStart(CardSlider);

            if (e.Parameter is CardsBundleContainer element)
            {
                CardsBundleContainer = element;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", CardSlider);
        }
    }
}