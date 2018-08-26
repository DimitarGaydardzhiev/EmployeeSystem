using DTOs.Models;

namespace ServiceLayer.Interfaces
{
    public interface IReportService
    {
        ChartDto GetReport(int id);
    }
}
