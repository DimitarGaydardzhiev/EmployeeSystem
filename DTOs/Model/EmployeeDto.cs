using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Models
{
    public class EmployeeDto : BaseDto
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

        public string Role { get; set; }

        [Required]
        [Display(Name = "Position")]
        public int PositionId { get; set; }

        public string Position { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public string Department { get; set; }

        [Required]
        [Display(Name = "Manager")]
        public int ManagerId { get; set; }

        [Display(Name = "Personal Description")]
        [MaxLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Starting Date")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartingDate { get; set; } = DateTime.Now.Date;

        [DataType(DataType.Date)]
        [Display(Name = "In Company To")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InCompanyTo { get; set; } = DateTime.Now.Date;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public bool IsActive { get; set; }
    }
}
