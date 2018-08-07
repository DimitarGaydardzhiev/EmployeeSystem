using DbEntities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DbEntities.Models
{
    public class EmployeePosition : IBase
    {
        public int Id { get; set; }

        [Required]
        public int Name { get; set; }
    }
}
