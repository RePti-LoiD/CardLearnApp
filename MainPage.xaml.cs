using CardLearnApp.Pages;
using System;
using System.Net;
using System.Net.Sockets;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace CardLearnApp
{
    public sealed partial class MainPage : Page
    {
        private string currentPageName;
        private static MainPage mainPage;

        public Page currentPage;

        public static MainPage Instance { get => mainPage; private set => mainPage = value; }

        public string CurrentPageName
        {
            get => currentPageName;
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
                InitSettingsConfiguration();
            };
        }

        private void InitSettingsConfiguration()
        {
            
        }

        private void Main_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            Microsoft.UI.Xaml.Controls.NavigationViewItem item = args.SelectedItem as Microsoft.UI.Xaml.Controls.NavigationViewItem;

            if (!args.IsSettingsSelected && item.Tag != null)
                NavigateFrame(item.Tag.ToString(), null);
            else if (args.IsSettingsSelected)
                NavigateFrame(item.Tag.ToString(), null);
        }

        private void MainFrame_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigateFrame("Home", null);
        }

        public void NavigateFrame(string name, object data)
        {
            string type = $"{nameof(CardLearnApp)}.{nameof(Pages)}.{name}";

            MainFrame.Navigate(Type.GetType(type), data, new DrillInNavigationTransitionInfo());
            MainNav.PaneTitle = name;
        }
    }
}