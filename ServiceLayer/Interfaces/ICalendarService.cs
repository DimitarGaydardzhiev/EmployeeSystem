using DTOs.Models;

namespace ServiceLayer.Interfaces
{
    public interface ICalendarService
    {
        CurrentMonthDto GetCurrentMonthData();
    }
}
