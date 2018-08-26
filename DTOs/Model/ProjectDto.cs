using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Models
{
    public class ProjectDto : BaseDto
    {
        [Required]
        public override string Name { get; set; }

        public List<MultiSelectDto> Employees { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
