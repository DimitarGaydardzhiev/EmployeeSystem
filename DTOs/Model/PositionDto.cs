using System.ComponentModel.DataAnnotations;

namespace DTOs.Models
{
    public class PositionDto : BaseDto
    {
        [Required]
        public override string Name { get; set; }

        public int EmployeesCount { get; set; }
    }
}
