using DbEntities.Interfaces;
using System.Collections.Generic;

namespace DbEntities.Models
{
    public class Department : IBase
    {
        public Department()
        {
            Employees = new HashSet<EmployeeUser>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<EmployeeUser> Employees { get; set; }
    }
}
