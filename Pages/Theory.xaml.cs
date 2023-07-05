using CardLearnApp.Data;
using CardLearnApp.DataPresentation.Theory;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CardLearnApp.Pages
{
    public sealed partial class Theory : Page
    {
        private BundleContainer bundleContainer;

        public Theory()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                if (bundleContainer != null && bundleContainer.TheoryDataContainers != null)
                    foreach (var item in bundleContainer.TheoryDataContainers)
                        BlocksGrid.Items.Add(new TheoryElement() { DataContainer = item });

                else
                    ReturnGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ConnectedAnimation anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (anim != null)
                anim.TryStart(Bar);

            if (e.Parameter is BundleContainer element)
                bundleContainer = element;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", Bar);
        }

        private void ReturnToMain(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainPage.Instance.NavigateFrame("Home", null);
        }
    }
}