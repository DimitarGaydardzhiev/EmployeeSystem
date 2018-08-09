using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOs.ViewModels
{
    public class EmployeeViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string RoleId { get; set; }

        public string Qualification { get; set; }

        [Required]
        [Display(Name = "Position")]
        public int PositionId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Starting Date")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartingDate { get; set; } = DateTime.Now.Date;
    }
}
