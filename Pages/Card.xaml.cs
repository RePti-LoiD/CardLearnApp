using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CardLearnApp.Pages
{
    public sealed partial class Card : Page
    {
        private List<CardElement> cards = new List<CardElement>();

        public Card()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                cards.Add(new CardElement() { Container = new CardDataContainer("Оборона Киева", "11 июля - 19 сентября") });
                cards.Add(new CardElement() { Container = new CardDataContainer("Приказ наркома обороны СССР №270.", "16 августа") });
                cards.Add(new CardElement() { Container = new CardDataContainer("Начало блокады Ленинграда", "8 сентября") });
                cards.Add(new CardElement() { Container = new CardDataContainer("Оборона Одессы", "5 августа - 16 октября") });
                
                CardSlider.ItemsSource = cards;
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ConnectedAnimation anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(CardSlider);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", CardSlider);
        }

        private void CardSlider_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            /*angle += 180;
            isFrontSide = !isFrontSide;

            Storyboard storyboard = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation
            {
                From = angle - 180,
                To = angle,
                Duration = new Duration(TimeSpan.FromSeconds(0.2f))
            };

            UIElement grid = CardSlider.ItemTemplate.GetElement(new ElementFactoryGetArgs() { Parent = CardSlider });

            var child = VisualTreeHelper.GetChild(grid, 0) as FrameworkElement;

            Projection projection = child.Projection;

            Storyboard.SetTarget(animation, projection);
            Storyboard.SetTargetProperty(animation, "RotationY");
            storyboard.Children.Clear();
            storyboard.Children.Add(animation);
            storyboard.Begin();*/
        }
    }
}