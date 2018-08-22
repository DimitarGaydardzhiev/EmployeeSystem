using DbEntities.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbEntities.Models
{
    public class EmployeePosition : IBase
    {
        public EmployeePosition()
        {
            Employees = new HashSet<EmployeeUser>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<EmployeeUser> Employees { get; set; }
    }
}
