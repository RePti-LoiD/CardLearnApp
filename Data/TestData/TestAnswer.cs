namespace CardLearnApp.Data.TestData
{
    public class TestAnswer
    {
        private string answerTitle;
        public string AnswerTitle { get => answerTitle; set => answerTitle = value; }


        private bool isCorrect;
        public bool IsCorrect { get => isCorrect; set => isCorrect = value; }

        private TestAnswer() { }

        public TestAnswer(string answerTitle, bool isCorrect)
        {
            this.answerTitle = answerTitle;
            this.isCorrect = isCorrect;
        }

        public override bool Equals(object obj)
        {
            if (obj is TestAnswer answer)
            {
                if (answer.AnswerTitle == this.AnswerTitle)
                {
                    return true;
                }
            }
            return false;
        }
    }
}