using DTOs.ViewModels;
using ServiceLayer.Interfaces;
using System;

namespace ServiceLayer.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly string[] Days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        public CurrentMonthViewModel GetCurrentMonthData()
        {
            var days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var result = new CurrentMonthViewModel()
            {
                Month = DateTime.Now.ToString("MMMM"),
                Days = days,
                FirstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).DayOfWeek.ToString(),
                Dates = FillDates(days)
            };

            return result;
        }

        // TODO: Fill previous and next month dates
        private byte[,] FillDates(int days)
        {
            byte[,] dates = new byte[5, 7];
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).DayOfWeek.ToString();
            var index = Array.IndexOf(Days, firstDayOfMonth);
            byte counter = 1;
            bool startFill = false;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (j == index)
                    {
                        startFill = true;
                    }

                    if (startFill)
                    {
                        dates[i, j] = counter;
                        counter++;
                        if (counter > days)
                        {
                            break;
                        }
                    }
                }

                if (counter > days)
                {
                    break;
                }
            }

            return dates;
        }
    }
}
