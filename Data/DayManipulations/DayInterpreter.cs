using System;

namespace CardLearnApp.Data.DayManipulations
{
    public class DayInterpreter
    {
        public static string GetDayJoke() => GetDayJoke((int)DateTime.Now.DayOfWeek);

        public static string GetDayJoke(int dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case 0:
                    return $"Воскресенье - день веселья!";
                case 1:
                    return $"Понедельник - день бездельник.";
                case 2:
                    return $"Вторник - повторник.";
                case 3:
                    return $"Среда - тамада.";
                case 4:
                    return $"В четверг я заботы все отверг.";
                case 5:
                    return $"Пятница - развратница.";
                case 6:
                    return $"Суббота - безработа!";

                default: return string.Empty;
            }
        }
    }
}