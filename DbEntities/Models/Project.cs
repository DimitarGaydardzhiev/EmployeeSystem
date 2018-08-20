using DbEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbEntities.Models
{
    public class Project : IBase
    {
        public Project()
        {
            EmployeeUserProjects = new HashSet<EmployeeUserProject>();
        }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public int ProjectStatusId { get; set; }

        public ProjectStatus Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public IEnumerable<EmployeeUserProject> EmployeeUserProjects { get; set; }
    }
}
