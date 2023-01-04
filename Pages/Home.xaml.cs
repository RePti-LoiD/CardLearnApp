using CardLearnApp.Data;
using CardLearnApp.Data.DayManipulations;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                MainDataSaveHandler.RestoreData();

                MainDataContainer mainDataContainer = MainDataContainer.Initialize();

                foreach (CardsBundleContainer card in mainDataContainer.Bundles)
                {
                    cardsArrayContainers.Add(new CardsBundleElement(card));
                }

                BasicGridView.ItemsSource = cardsArrayContainers;

                WelcomeText.Text = DayInterpreter.GetDayJoke();
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