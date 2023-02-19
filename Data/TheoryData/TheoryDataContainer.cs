namespace CardLearnApp.Data.TheoryData
{
    public class TheoryDataContainer
    {
        private string theoryBlockName = "Header";
        private string theoryBlockDescription = "Lorem ipsum";
        private string linkToPic;

        public string TheoryBlockName { get => theoryBlockName; set => theoryBlockName = value; }
        public string TheoryBlockDescription { get => theoryBlockDescription; set => theoryBlockDescription = value; }
        public string LinkToPic { get => linkToPic; set { linkToPic = value; } }

        public TheoryDataContainer(string theoryBlockName, string theoryBlockDescription, string linkToPic)
        {
            TheoryBlockName = theoryBlockName;
            TheoryBlockDescription = theoryBlockDescription;
            LinkToPic = linkToPic;
        }
    }
}