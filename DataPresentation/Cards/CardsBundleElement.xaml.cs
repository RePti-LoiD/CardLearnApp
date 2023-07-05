using CardLearnApp.Data.TestData;
using CardLearnApp.Pages;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

namespace CardLearnApp.Data
{
    public sealed partial class CardsBundleElement : UserControl
    {
        private int angle;
        private bool isFrontSide;

        public BundleContainer Container;

        public delegate void BundleOpen(UIElement sender);
        public event BundleOpen OnBundleOpen;

        public delegate void BundleDelete(CardsBundleElement sender);
        public event BundleDelete OnBundleDelete;

        public CardsBundleElement(BundleContainer container)
        {
            InitializeComponent();

            Container = container;

            Loaded += (x, y) =>
            {
                if (container.CardDataContainers.Count >= 2)
                {
                    Card1.Visibility = Visibility.Visible;
                    Card2.Visibility = Visibility.Visible;

                    FCardText.Text = container.CardDataContainers[0].FrontSideText;
                    SCardText.Text = container.CardDataContainers[1].FrontSideText;
                }
                else
                {
                    TextOnEmpty.Visibility = Visibility.Visible;
                }

                DataContext = container;
            };
        }

        public void OnCardClick()
        {
            angle += 180;
            isFrontSide = !isFrontSide;

            FrontSide.Visibility = (Visibility)Convert.ToInt32(isFrontSide);
            BackSide.Visibility = (Visibility)Convert.ToInt32(!isFrontSide);

            Storyboard storyboard = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation
            {
                From = angle - 180,
                To = angle,
                Duration = new Duration(TimeSpan.FromSeconds(0.2f))
            };

            Storyboard.SetTarget(animation, Proje);
            Storyboard.SetTargetProperty(animation, "RotationY");
            storyboard.Children.Clear();
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

        private void EditButtonClicked(object sender, RoutedEventArgs e)
        {
            OnBundleOpen?.Invoke(this);

            MainPage.Instance.NavigateFrame(nameof(Edit), Container);
        }

        private void OpenButtonClicked(object sender, RoutedEventArgs e)
        {
            OnBundleOpen?.Invoke(this);

            MainPage.Instance.NavigateFrame(nameof(Card), Container);
        }

        private void TheoryButtonClicked(object sender, RoutedEventArgs e)
        {
            OnBundleOpen?.Invoke(this);

            MainPage.Instance.NavigateFrame(nameof(Theory), Container);
        }

        private void TestClicked(object sender, RoutedEventArgs e)
        {
            OnBundleOpen?.Invoke(this);

            MainPage.Instance.NavigateFrame(nameof(Test), TestGenerator.GenerateTest(Container));
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            OnCardClick();
        }

        public void TriggerOpen() =>
            OpenButtonClicked(null, null);

        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            OnBundleDelete?.Invoke(this);
        }
    }
}