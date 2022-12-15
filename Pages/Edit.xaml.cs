using CardLearnApp.Data;
using CardLearnApp.DataPresentation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CardLearnApp.Pages
{
    public sealed partial class Edit : Page
    {
        private CardsBundleContainer dataContainer;

        private ObservableCollection<CardEditorNode> nodes = new ObservableCollection<CardEditorNode>();

        public Edit()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                if (dataContainer != null)
                {
                    foreach (CardDataContainer item in dataContainer.CardDataContainers)
                    {
                        CardEditorNode cardEditorNode = new CardEditorNode() { DataContainer = item };
                        
                        cardEditorNode.OnRemoveSelf += (removableItem) => {
                            nodes.Remove(removableItem);    
                        };

                        nodes.Add(cardEditorNode);
                    }
                }

                NodeList.ItemsSource = nodes;
            };
        }

        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            MainDataContainer mainDataContainer = MainDataContainer.Initialize();

            int index = mainDataContainer.Bundles.IndexOf(dataContainer);

            List<CardDataContainer> cardDataContainers = new List<CardDataContainer>();

            foreach (CardEditorNode item in nodes)
                cardDataContainers.Add(item.DataContainer);

            mainDataContainer.Bundles[index].CardDataContainers = cardDataContainers;
            mainDataContainer.Bundles[index].BundleName = BundleNameTextBox.Text;
            mainDataContainer.Bundles[index].BundleDescription = BundleDescriptionTextBox.Text;

            MainDataSaveHandler.SaveAllData();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null || e.Parameter is CardsBundleContainer cardsBundle)
            {
                cardsBundle = e.Parameter as CardsBundleContainer;

                BundleNameTextBox.Text = cardsBundle.BundleName;
                BundleDescriptionTextBox.Text = cardsBundle.BundleDescription;

                dataContainer = cardsBundle;
            }
            else
            {
                dataContainer = new CardsBundleContainer();
            }
        }

        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            MainDataContainer mainDataContainer = MainDataContainer.Initialize();

            if (mainDataContainer.Bundles.Remove(dataContainer))
            {
                MainDataSaveHandler.SaveAllData();

                MainPage.Instance.NavigateFrame(nameof(Home), null);
            }
        }

        private void AddNewItem(object sender, RoutedEventArgs e)
        {
            CardEditorNode cardEditorNode = new CardEditorNode() { DataContainer = new CardDataContainer("New", "New") };

            cardEditorNode.OnRemoveSelf += (removableItem) => {
                nodes.Remove(removableItem);
            };

            nodes.Add(cardEditorNode );

            NodeList.ItemsSource = nodes;
            NodeList.CompleteViewChange();
        }
    }
}