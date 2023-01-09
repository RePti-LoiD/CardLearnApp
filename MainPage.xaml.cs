using System;
using Windows.UI.Xaml.Controls;

namespace CardLearnApp
{
    public sealed partial class MainPage : Page
    {
        private string currentPageName;
        private static MainPage mainPage;

        public static MainPage Instance { get => mainPage; private set => mainPage = value; }

        public string CurrentPageName { get => currentPageName;
            private set
            {
                currentPageName = value;
                MainNav.PaneTitle = value;
            }
        }

        public MainPage()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                mainPage = this;
            };
        }

        private void Main_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (!args.IsSettingsSelected)
                NavigateFrame((args.SelectedItem as Microsoft.UI.Xaml.Controls.NavigationViewItem).Tag.ToString(), null);
        }

        private void MainFrame_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigateFrame("Home", null);
        }

        public void NavigateFrame(string name, object data)
        {
            string type = $"{nameof(CardLearnApp)}.{nameof(Pages)}.{name}";

            MainFrame.Navigate(Type.GetType(type), data);
            MainNav.PaneTitle = name;
        }
    }
}