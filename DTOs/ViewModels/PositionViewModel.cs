using System.ComponentModel.DataAnnotations;

namespace DTOs.ViewModels
{
    public class PositionViewModel : BaseViewModel
    {
        [Required]
        public override string Name { get; set; }

        public int EmployeesCount { get; set; }
    }
}
