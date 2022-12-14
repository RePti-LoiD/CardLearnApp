using CardLearnApp.Data;
using Windows.UI.Xaml.Controls;

namespace CardLearnApp.Pages
{
    public sealed partial class Edit : Page
    {
        public Edit()
        {
            InitializeComponent();
        }

        private void SaveButtonClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainDataSaveHandler.SaveAllData();
        }
    }
}