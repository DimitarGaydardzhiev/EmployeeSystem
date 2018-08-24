using System.ComponentModel;

namespace DTOs.Enums
{
    public enum ReportType
    {
        [Description("Employees By Positions")]
        EmployeesByPositions = 1,

        [Description("Project Assigned Employees")]
        ProjectAssignedEmployees = 2
    }
}
