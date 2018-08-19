using System.ComponentModel.DataAnnotations;

namespace DTOs.ViewModels
{
    public class DepartmentViewModel : BaseViewModel
    {
        [Required]
        public override string Name { get; set; }

        public int EmployeesCount { get; set; }
    }
}
