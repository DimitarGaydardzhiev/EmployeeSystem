using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOs.ViewModels
{
    public class EmployeeViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Role { get; set; }

        public string JobTitle { get; set; }

        public string Qualification { get; set; }

        public IEnumerable<BaseViewModel> Roles { get; set; }
    }
}
