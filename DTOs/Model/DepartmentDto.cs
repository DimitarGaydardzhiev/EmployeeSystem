using System.ComponentModel.DataAnnotations;

namespace DTOs.Models
{
    public class DepartmentDto : BaseDto
    {
        [Required]
        public override string Name { get; set; }

        public int EmployeesCount { get; set; }
    }
}
