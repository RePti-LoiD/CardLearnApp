using System;
using Windows.UI.Xaml.Controls;

namespace CardLearnApp
{
    public sealed partial class MainPage : Page
    {
        private string currentPageName;

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
        }

        private void Main_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (!args.IsSettingsSelected)
                NavigatePageByName((args.SelectedItem as Microsoft.UI.Xaml.Controls.NavigationViewItem).Tag.ToString());
        }

        private void NavigatePageByName(string name)
        {
            string type = $"{nameof(CardLearnApp)}.{nameof(Pages)}.{name}";

            MainFrame.Navigate(Type.GetType(type));
            MainNav.PaneTitle = name;
        }

        private void MainFrame_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigatePageByName("Home");
        }
    }
}