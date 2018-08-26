using System;

namespace EmployeeSystem.Models
{
    public class ErrorModelDto
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}