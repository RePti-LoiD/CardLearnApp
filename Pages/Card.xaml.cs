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

        public BundleContainer CardsBundleContainer { get; set; }
        public Card()
        {
            InitializeComponent();

            CardSlider.Loaded += (x, y) =>
            {
                if (CardsBundleContainer != null)
                {
                    cardElements = new List<CardElement>();

                    foreach (CardDataContainer item in CardsBundleContainer.CardDataContainers)
                    {
                        cardElements.Add(new CardElement() { Container = item, IsLearn = item.IsLearned });
                    }

                    CardSlider.ItemsSource = cardElements;

                    BundleNameTxt.Text = CardsBundleContainer.BundleName;
                    BundleDesc.Text = CardsBundleContainer.BundleDescription;


                    LearnButton.Click += (x1, y1) =>
                    {
                        cardElements[CardSlider.SelectedIndex].IsLearn = !cardElements[CardSlider.SelectedIndex].IsLearn;
                        cardElements[CardSlider.SelectedIndex].Container.IsLearned = (bool)LearnButton.IsChecked;
                    };

                    CardSlider.SelectionChanged += (x2, y2) =>
                    {
                        if (CardSlider.SelectedIndex < cardElements.Count && CardSlider.SelectedIndex >= 0)
                        {
                            LearnButton.IsChecked = cardElements[CardSlider.SelectedIndex].IsLearn;

                            Progress.Value = CardSlider.SelectedIndex * 100 / (cardElements.Count - 1);
                        }
                    };

                    ReturnGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }

                else
                {
                    MainGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ConnectedAnimation anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (anim != null)
                anim.TryStart(CardSlider);

            if (e.Parameter is BundleContainer element)
            {
                CardsBundleContainer = element;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", Icon);
        }

        private void ReturnToMain(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainPage.Instance.NavigateFrame("Home", null);
        }
    }
}