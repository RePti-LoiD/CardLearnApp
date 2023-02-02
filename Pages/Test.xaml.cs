using CardLearnApp.Data.TestData;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CardLearnApp.Pages
{
    public sealed partial class Test : Page
    {
        public List<TestQuestion> testQuestions = new List<TestQuestion>();

        private UIElement transitionTarget;
        private int currentQuestion = 0;

        public Test()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                if (testQuestions.Count == 0)
                {
                    MainGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ReturnGrid.Visibility = Visibility.Collapsed;
                }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is TestDataContainer && (e.Parameter as TestDataContainer).Questions.Count > 0)
            {
                testQuestions = (e.Parameter as TestDataContainer).Questions;
                ChangeData(0, neutral);
            }

            if (testQuestions.Count == 0)
                transitionTarget = ReturnGrid;
            else
                transitionTarget = MainGrid;

            ConnectedAnimation anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(transitionTarget);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", transitionTarget);
        }

        private void AnswerButtonClicked(object sender, RoutedEventArgs e)
        {
            CheckAnswer(testQuestions[currentQuestion], testQuestions[currentQuestion].Answers[int.Parse((sender as Button).Tag.ToString())]);
        }

        private void CheckAnswer(TestQuestion question, TestAnswer answer)
        {
            question.LastAnswer = answer;

            if (question.LastAnswer != null)
            {
                if (question.LastAnswer.IsCorrect)
                    correctAnswer.Begin();
                else if (!question.LastAnswer.IsCorrect)
                    badAnswer.Begin();
            }
            else
            {
                neutral.Begin();
            }
        }

        private void PrevButtonClicked(object sender, RoutedEventArgs e)
        {
            currentQuestion = Math.Clamp(currentQuestion - 1, 0, testQuestions.Count - 1);
            ChangeData(currentQuestion, Next);
        }

        private void NextButtonClicked(object sender, RoutedEventArgs e)
        {
            currentQuestion = Math.Clamp(currentQuestion + 1, 0, testQuestions.Count - 1);
            ChangeData(currentQuestion, Prev);
        }

        private void ChangeData(int index, Storyboard anim)
        {
            FrontSide.Text = testQuestions[index].QuestionTitle;

            Answer1Text.Text = testQuestions[index].Answers[0].AnswerTitle;
            Answer2Text.Text = testQuestions[index].Answers[1].AnswerTitle;
            Answer3Text.Text = testQuestions[index].Answers[2].AnswerTitle;
            Answer4Text.Text = testQuestions[index].Answers[3].AnswerTitle;

            Progress.Value = index * 100 / (testQuestions.Count - 1);

            CheckAnswer(testQuestions[index], testQuestions[index].LastAnswer);

            anim.Begin();
        }

        private void ReturnToMain(object sender, RoutedEventArgs e)
        {
            MainPage.Instance.NavigateFrame("Home", null);
        }
    }
}