using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

namespace CardLearnApp.Pages
{
    public sealed partial class CardElement : UserControl
    {
        private int angle;
        private bool isFrontSide;

        private bool isLearn;

        public bool IsLearn
        {
            get => isLearn;
            set
            {
                if (value)
                {
                    Icon.Visibility = Visibility.Collapsed;
                    Icon2.Visibility = Visibility.Visible;
                }
                else
                {
                    Icon.Visibility = Visibility.Visible;
                    Icon2.Visibility = Visibility.Collapsed;
                }
                isLearn = value;
            }
        }

        private CardDataContainer container;

        public CardDataContainer Container { get => container; set { container = value;  UpdateContext(); } }

        public CardElement()
        {
            InitializeComponent();
        }

        private void UpdateContext()
        {
            RotationObj.DataContext = container;
        }

        private void OnCardClick(object sender, TappedRoutedEventArgs e)
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
    }
}