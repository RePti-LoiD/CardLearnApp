using CardLearnApp.Data;
using CardLearnApp.DataPresentation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CardLearnApp.Pages
{
    public sealed partial class Edit : Page
    {
        private CardsBundleContainer dataContainer;

        private ObservableCollection<CardEditorNode> nodes = new ObservableCollection<CardEditorNode>();

        private bool bundleRecieve;

        public Edit()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                if (dataContainer != null && dataContainer.CardDataContainers != null)
                {
                    foreach (CardDataContainer item in dataContainer.CardDataContainers)
                    {
                        CardEditorNode cardEditorNode = new CardEditorNode() { DataContainer = item };
                        
                        cardEditorNode.OnRemoveSelf += (removableItem) => {
                            nodes.Remove(removableItem);    
                        };

                        nodes.Add(cardEditorNode);
                    }

                    DeadlineDate.Date = dataContainer.DeadLine.Date;
                    MainPivot.Title = dataContainer.DeadLine.Date;
                }

                NodeList.ItemsSource = nodes;
            };
        }

        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            if (bundleRecieve)
            {
                SaveCurrentData();
            }
            else
            {
                MainDataContainer mainDataContainer = MainDataContainer.Initialize();
                mainDataContainer.Bundles.Add(new CardsBundleContainer() 
                { 
                    CardDataContainers = GetContainers(nodes), 
                    BundleDescription = BundleDescriptionTextBox.Text,
                    BundleName = BundleNameTextBox.Text
                });

                MainDataSaveHandler.SaveAllData();
            }
        }

        private void SaveCurrentData()
        {
            MainDataContainer mainDataContainer = MainDataContainer.Initialize();

            int index = mainDataContainer.Bundles.IndexOf(dataContainer);

            mainDataContainer.Bundles[index].CardDataContainers = GetContainers(nodes);
            mainDataContainer.Bundles[index].BundleName = BundleNameTextBox.Text;
            mainDataContainer.Bundles[index].BundleDescription = BundleDescriptionTextBox.Text;

            MainDataSaveHandler.SaveAllData();
        }

        private List<CardDataContainer> GetContainers(ObservableCollection<CardEditorNode> cardDataContainers)
        {
            List<CardDataContainer> bundleContainer = new List<CardDataContainer>();

            foreach (CardEditorNode item in cardDataContainers)
                bundleContainer.Add(item.DataContainer);

            return bundleContainer;
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

                bundleRecieve = true;
            }
            else
            {
                dataContainer = new CardsBundleContainer();

                bundleRecieve = false;
            }

            ConnectedAnimation anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(MainPivot);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", MainPivot);
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

        private void DeadlineTimeChange(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            dataContainer.DeadLine = sender.Date;
        }

        private void DeadlineTimeLoaded(object sender, RoutedEventArgs e)
        {
            (sender as DatePicker).Date = dataContainer.DeadLine;
        }
    }
}