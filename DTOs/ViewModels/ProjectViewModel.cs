using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOs.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        [Required]
        public override string Name { get; set; }

        public List<MultiSelectViewModel> Employees { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        //[DisplayFormat(DataFormatString = "{dd-yyyy-mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
