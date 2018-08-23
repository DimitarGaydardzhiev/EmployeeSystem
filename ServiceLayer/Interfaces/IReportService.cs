using EmployeeSystem.Areas.AdminControlls.Models;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IReportService
    {
        IEnumerable<DataPoint> GetEmpoyeesData();
    }
}
