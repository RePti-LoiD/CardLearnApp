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

        public CardsBundleContainer Container;

        public CardsBundleElement(CardsBundleContainer container)
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

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            OnCardClick();
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
            MainPage.Instance.NavigateFrame(nameof(Edit), Container);
        }

        private void OpenButtonClicked(object sender, RoutedEventArgs e)
        {
            MainPage.Instance.NavigateFrame(nameof(Card), Container);
        }
    }
}