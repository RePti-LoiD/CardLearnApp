using CardLearnApp.Data;
using CardLearnApp.Data.DayManipulations;
using System.Collections.Generic;
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
    }
}