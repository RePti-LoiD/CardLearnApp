using CardLearnApp.Data;
using CardLearnApp.Data.TheoryData;
using CardLearnApp.DataPresentation;
using CardLearnApp.DataPresentation.Theory;
using System;
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
        private BundleContainer dataContainer;

        private ObservableCollection<CardEditorNode> nodes = new ObservableCollection<CardEditorNode>();
        private ObservableCollection<TheoryEditorNode> theoryNodes = new ObservableCollection<TheoryEditorNode>();

        private bool bundleRecieve;

        public Edit()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                if (dataContainer != null)
                {
                    DrawCardNodes();
                    DrawTheoryNodes();
                }

                DeadlineDate.Date = dataContainer.DeadLine.Date;

                TheoryNodeList.ItemsSource = theoryNodes;
                NodeList.ItemsSource = nodes;
            };
        }

        private void DrawTheoryNodes()
        {
            if (dataContainer.TheoryDataContainers != null)
            {
                foreach (TheoryDataContainer item in dataContainer.TheoryDataContainers)
                {
                    TheoryEditorNode theoryEditorNode = new TheoryEditorNode() { DataContainer = item };

                    theoryEditorNode.OnRemoveSelf += (removableItem) =>
                    {
                        theoryNodes.Remove(removableItem);
                    };

                    theoryNodes.Add(theoryEditorNode);
                }
            }
        }

        private void DrawCardNodes()
        {
            if (dataContainer.CardDataContainers != null)
            {
                foreach (CardDataContainer item in dataContainer.CardDataContainers)
                {
                    CardEditorNode cardEditorNode = new CardEditorNode() { DataContainer = item };

                    cardEditorNode.OnRemoveSelf += (removableItem) =>
                    {
                        nodes.Remove(removableItem);
                    };

                    nodes.Add(cardEditorNode);
                }
            }
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
                mainDataContainer.Bundles.Add(new BundleContainer()
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
            mainDataContainer.Bundles[index].TheoryDataContainers = GetContainers(theoryNodes);
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
        
        private List<TheoryDataContainer> GetContainers(ObservableCollection<TheoryEditorNode> theoryDataContainers)
        {
            List<TheoryDataContainer> bundleContainer = new List<TheoryDataContainer>();

            foreach (TheoryEditorNode item in theoryDataContainers)
                bundleContainer.Add(item.DataContainer);

            return bundleContainer;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null || e.Parameter is BundleContainer)
            {
                dataContainer = e.Parameter as BundleContainer;

                bundleRecieve = true;
            }
            else
            {
                dataContainer = new BundleContainer();

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

        private async void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            MainDataContainer mainDataContainer = MainDataContainer.Initialize();

            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = XamlRoot;
            dialog.Title = "Are you sure you want to delete the bundle?";
            dialog.PrimaryButtonText = "Delete";
            dialog.SecondaryButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (mainDataContainer.Bundles.Remove(dataContainer))
                {
                    MainDataSaveHandler.SaveAllData();

                    MainPage.Instance.NavigateFrame(nameof(Home), null);
                }
            }
        }

        private void AddNewItem(object sender, RoutedEventArgs e)
        {
            CardEditorNode cardEditorNode = new CardEditorNode() { DataContainer = new CardDataContainer("New", "New") };

            cardEditorNode.OnRemoveSelf += (removableItem) => 
            {
                nodes.Remove(removableItem);
            };

            nodes.Add(cardEditorNode );

            NodeList.ItemsSource = nodes;
            NodeList.CompleteViewChange();
        }

        private void AddNewTheoryItem(object sender, RoutedEventArgs e)
        {
            TheoryEditorNode theoryEditorNode = new TheoryEditorNode() { DataContainer = new TheoryDataContainer("header", "lorem ipsum", "https://raw.githubusercontent.com/RePti-LoiD/GachiPage/main/HtmlCssGachiPage/BankLogo.png") };

            theoryEditorNode.OnRemoveSelf += (removableItem) =>
            {
                theoryNodes.Remove(removableItem);
            };

            theoryNodes.Add(theoryEditorNode);

            TheoryNodeList.ItemsSource = theoryNodes;
            TheoryNodeList.CompleteViewChange();
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