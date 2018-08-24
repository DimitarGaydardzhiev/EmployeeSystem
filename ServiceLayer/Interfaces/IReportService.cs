using DTOs.ViewModels;

namespace ServiceLayer.Interfaces
{
    public interface IReportService
    {
        ChartViewModel GetReport(int id);
    }
}
