using DTOs.Models;
using ServiceLayer.Interfaces;
using System;

namespace ServiceLayer.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly string[] Days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        public CurrentMonthDto GetCurrentMonthData()
        {
            var days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var result = new CurrentMonthDto()
            {
                Month = DateTime.Now.ToString("MMMM"),
                Days = days,
                FirstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).DayOfWeek.ToString(),
                Dates = FillDates(days)
            };

            return result;
        }

        private byte[,] FillDates(int days)
        {
            byte[,] dates = new byte[5, 7];
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).DayOfWeek.ToString();
            var firstDayIndex = Array.IndexOf(Days, firstDayOfMonth);

            FillCurrentMonth(days, firstDayIndex, dates);

            FillPreviousMonthDates(firstDayIndex, dates);

            FillNextMonthDates(dates);

            return dates;
        }

        private void FillCurrentMonth(int days, int firstDayIndex, byte[,] dates)
        {
            byte counter = 1;
            bool startFill = false;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (j == firstDayIndex)
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
        }

        private void FillPreviousMonthDates(int firstDayIndex, byte[,] dates)
        {
            var prevMonthName = DateTime.Now.AddMonths(-1).ToString("MMMM");
            var prevMonthDays = byte.Parse(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1).ToString());
            while (firstDayIndex != 0)
            {
                dates[0, firstDayIndex - 1] = prevMonthDays;
                prevMonthDays--;
                firstDayIndex--;
            }
        }

        private void FillNextMonthDates(byte[,] dates)
        {
            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var lastDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, daysInMonth).DayOfWeek.ToString();
            var lastDayIndex = Array.IndexOf(Days, lastDayOfMonth);
            byte daysCounter = 1;
            while (lastDayIndex != 6)
            {
                dates[4, lastDayIndex + 1] = daysCounter;
                daysCounter++;
                lastDayIndex++;
            }
        }
    }
}
