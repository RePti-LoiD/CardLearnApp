using CardLearnApp.Data;
using CardLearnApp.Data.DataSeach;
using CardLearnApp.DataPresentation.Cards;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CardLearnApp.Pages
{
    public sealed partial class Card : Page
    {
        private List<CardElement> cardElements = new List<CardElement>();
        private List<MiniCardElement> miniCardElements = new List<MiniCardElement>();

        public BundleContainer CardsBundleContainer { get; set; }
        public Card()
        {
            InitializeComponent();

            CardSlider.Loaded += (x, y) =>
            {
                if (CardsBundleContainer != null)
                {
                    for (int i = 0; i < CardsBundleContainer.CardDataContainers.Count; i++)
                    {
                        cardElements.Add(new CardElement() 
                        { 
                            Container = CardsBundleContainer.CardDataContainers[i], 
                            IsLearn = CardsBundleContainer.CardDataContainers[i].IsLearned 
                        });

                        miniCardElements.Add(new MiniCardElement(CardsBundleContainer.CardDataContainers[i], i + 1));
                    }

                    SetAllMiniCards();
                    CardSlider.ItemsSource = cardElements;

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

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(sender.Text))
            {
                SetAllMiniCards();
                return;
            }

            MiniCards.Items.Clear();
            List<string> names = new List<string>();

            foreach (MiniCardElement item in miniCardElements)
                names.Add(item.Container.FrontSideText);

            List<string> results = SearchElement.ByName(names, sender.Text);

            foreach (string resultStr in results)
                foreach (MiniCardElement miniCardElement in miniCardElements)
                    if (resultStr == miniCardElement.Container.FrontSideText)
                        MiniCards.Items.Add(miniCardElement);
        }

        private void SetAllMiniCards()
        {
            MiniCards.Items.Clear();

            foreach (MiniCardElement item in miniCardElements)
                MiniCards.Items.Add(item);
        }
    }
}