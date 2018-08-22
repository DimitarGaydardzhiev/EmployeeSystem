using DbEntities.Interfaces;
using EmployeeSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities.Models
{
    public class EmployeeUser : IBase
    {
        public EmployeeUser()
        {
            Requests = new HashSet<Request>();
            EmployeeUserProjects = new HashSet<EmployeeUserProject>();
            InCompanyTo = DateTime.Now;
        }

        public int Id { get; set; }

        public bool IsActive { get; set; }
        
        public DateTime? Birthday { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? InCompanyFrom { get; set; }
        
        public DateTime? InCompanyTo { get; set; }

        public int? Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalDescription { get; set; }

        public string AspUserId { get; set; }
        
        public int? DepartmentId { get; set; }

        public int? EmployeePositionId { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }

        public EmployeeUser Manager { get; set; }

        public AspUser AspUser { get; set; }

        public Department Department { get; set; }

        public EmployeePosition EmployeePosition { get; set; }

        public IEnumerable<Request> Requests { get; set; }

        public IEnumerable<EmployeeUserProject> EmployeeUserProjects { get; set; }
    }
}
