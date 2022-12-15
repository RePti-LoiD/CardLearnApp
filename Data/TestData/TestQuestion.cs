using System.Collections.Generic;

namespace CardLearnApp.Data.TestData
{
    public class TestQuestion
    {
        private string title;
        public string QuestionTitle { get => title; set => title = value; }

        private List<TestAnswer> answers = new List<TestAnswer>();
        public List<TestAnswer> Answers { get => answers; set => answers = value; }

        private TestAnswer lastAnswer;
        public TestAnswer LastAnswer { get => lastAnswer; set => lastAnswer = value; }

        public TestQuestion() { }

        public TestQuestion(string title, List<TestAnswer> answers)
        {
            QuestionTitle = title;
            Answers = answers;
        }

        public bool SetAnswer(TestAnswer testAnswer)
        {
            LastAnswer = testAnswer;

            return LastAnswer.IsCorrect;
        }
    }
}
