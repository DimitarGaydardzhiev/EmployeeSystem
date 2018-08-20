using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOs.ViewModels
{
    public class ProjectViewModel
    {
        [Required]
        public string Name { get; set; }

        public List<MultiSelectViewModel> Employees { get; set; }

        public string Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
