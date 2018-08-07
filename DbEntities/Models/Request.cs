﻿using DbEntities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DbEntities.Models
{
    public class Request : IBase
    {
        public int Id { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        public int RequestTypeId { get; set; }

        public int EmployeeUserId { get; set; }

        public RequestType RequestType { get; set; }

        public EmployeeUser EmployeeUser { get; set; }
    }
}