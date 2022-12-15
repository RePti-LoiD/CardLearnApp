using CardLearnApp.Pages;
using System;
using System.Collections.Generic;

namespace CardLearnApp.Data.TestData
{
    public class TestGenerator
    {
        private static Random random = new Random();

        public static TestDataContainer GenerateTest(CardsBundleContainer cardsBundleContainer)
        {
            if (cardsBundleContainer.CardDataContainers.Count < 4)
                return new TestDataContainer();

            List<TestQuestion> questions = new List<TestQuestion>();

            foreach (CardDataContainer item in cardsBundleContainer.CardDataContainers)
            {
                TestQuestion question = new TestQuestion();
                question.QuestionTitle = item.FrontSideText;
                question.Answers.Add(new TestAnswer(item.BackSideText, true));


                int[] selectedIndexes = new int[3] { -1, -1, -1 };
                
                for (int i = 0; i < 3; i++)
                {
                    int index;
                    do
                    {
                        index = random.Next(0, cardsBundleContainer.CardDataContainers.Count - 1);
                    }
                    while (IndexContains(selectedIndexes, index));
                    selectedIndexes[i] = index;
                }

                foreach (int i in selectedIndexes)
                    question.Answers.Add(new TestAnswer(cardsBundleContainer.CardDataContainers[i].BackSideText, false));

                question.Answers.Shuffle();

                questions.Add(question);
            }

            return new TestDataContainer(questions);
        }

        private static bool IndexContains(int[] array, int index)
        {
            foreach (int i in array)
                if (i == index)
                    return true;

            return false;
        }
    }
}