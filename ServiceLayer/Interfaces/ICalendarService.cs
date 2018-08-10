using DTOs.ViewModels;

namespace ServiceLayer.Interfaces
{
    public interface ICalendarService
    {
        CurrentMonthViewModel GetCurrentMonthData();
    }
}
