using CardLearnApp.Data;
using CardLearnApp.Data.DataSeach;
using CardLearnApp.Data.DayManipulations;
using System.Collections.Generic;
using System.Linq;
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

        private UIElement navObj;

        public Home()
        {
            InitializeComponent();

            BasicGridView.Loaded += (x, y) =>
            {
                navObj = Icon;

                MainDataSaveHandler.RestoreData();

                MainDataContainer mainDataContainer = MainDataContainer.Initialize();

                foreach (BundleContainer card in mainDataContainer.Bundles)
                {
                    CardsBundleElement cardsBundleElement = new CardsBundleElement(card);
                    cardsBundleElement.OnBundleOpen += (nav) =>
                    {
                        navObj = nav;
                    };

                    cardsArrayContainers.Add(cardsBundleElement);
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

            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", navObj);
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                foreach (var item in cardsArrayContainers)
                {
                    if (item.Container.BundleName == args.QueryText)
                    {
                        item.TriggerOpen();

                        break;
                    }
                }
            }
            catch (System.Exception) { }
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason != AutoSuggestionBoxTextChangeReason.UserInput) return;

            List<string> names = new List<string>();

            for (int i = 0; i < cardsArrayContainers.Count; i++) names.Add(cardsArrayContainers[i].Container.BundleName);

            sender.ItemsSource = SearchElement.ByName(names, sender.Text);
        }
    }
}