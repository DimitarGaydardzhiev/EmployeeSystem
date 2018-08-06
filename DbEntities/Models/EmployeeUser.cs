using DbEntities.Interfaces;
using EmployeeSystem.Models;

namespace DbEntities.Models
{
    public class EmployeeUser : IBase
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public AspUser AspUser { get; set; }
    }
}
