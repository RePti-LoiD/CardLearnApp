using System;
using System.Collections.Generic;

namespace CardLearnApp.Data.TestData
{
    public class TestGenerator
    {
        private static Random random = new Random();

        public static TestDataContainer GenerateTest(BundleContainer cardsBundleContainer)
        {
            if (cardsBundleContainer.CardDataContainers.Count < 4)
                return new TestDataContainer();

            List<TestQuestion> questions = new List<TestQuestion>();

            for (int j = 0; j < cardsBundleContainer.CardDataContainers.Count; j++)
            {
                TestQuestion question = new TestQuestion()
                {
                    QuestionTitle = cardsBundleContainer.CardDataContainers[j].FrontSideText,
                };
                question.Answers.Add(new TestAnswer(cardsBundleContainer.CardDataContainers[j].BackSideText, true));


                int[] pickedQuestions = GetInts(3, 0, cardsBundleContainer.CardDataContainers.Count, j);

                foreach (var item in pickedQuestions)
                    question.Answers.Add(new TestAnswer(cardsBundleContainer.CardDataContainers[item].BackSideText, false));

                question.Answers.Shuffle();

                questions.Add(question);
            }
            return new TestDataContainer(questions);
        }

        private static int[] GetInts(int count, int lBound, int rBound, int except)
        {
            int randomValue;
            int[] ints = new int[count];

            for (int i = 0; i < ints.Length; i++)
            {
                do
                {
                    randomValue = random.Next(lBound, rBound);
                }
                while (IsUniqie(randomValue, ints) || randomValue == except);

                ints[i] = randomValue;
            }

            return ints;
        }

        private static bool IsUniqie(int value, int[] array)
        {
            foreach (int i in array)
                if (i == value) return true;

            return false;
        }
    }
}