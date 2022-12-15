using System.Collections.Generic;

namespace CardLearnApp.Data.TestData
{
    public class TestDataContainer
    {
        private List<TestQuestion> questions = new List<TestQuestion>();
        public List<TestQuestion> Questions { get => questions; set => questions = value; }


        public TestDataContainer() { }

        public TestDataContainer(List<TestQuestion> answers)
        {
            Questions = answers;
        }
    }
}