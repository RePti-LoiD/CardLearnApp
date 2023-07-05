using System.Collections.Generic;

namespace CardLearnApp.Data.TestData
{
    public class TestQuestion
    {
        private string title;
        private int answerRetryCounter = 3;

        public delegate void CounterChange(int value);
        public event CounterChange OnCounterChange;

        public string QuestionTitle { get => title; set => title = value; }

        private List<TestAnswer> answers = new List<TestAnswer>();
        public List<TestAnswer> Answers { get => answers; set => answers = value; }

        private TestAnswer lastAnswer;
        public TestAnswer LastAnswer
        {
            get => lastAnswer;
            set
            {

                if (answerRetryCounter > 0)
                {
                    lastAnswer = value;

                    if (lastAnswer != null && !lastAnswer.IsCorrect)
                        AnswerRetryCounter--;
                }
            }
        }
        public int AnswerRetryCounter
        {
            get => answerRetryCounter;
            private set
            {
                answerRetryCounter = value;

                OnCounterChange?.Invoke(answerRetryCounter);
            }
        }

        public TestQuestion() { }

        public TestQuestion(string title, List<TestAnswer> answers)
        {
            QuestionTitle = title;
            Answers = answers;
        }
    }
}
