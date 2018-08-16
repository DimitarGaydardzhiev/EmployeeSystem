using System;
using System.ComponentModel.DataAnnotations;

namespace DTOs.ViewModels
{
    public class RequestViewModel
    {
        [Required]
        public DateTime From { get; set; }

        [Required]
        //[Compare("From", ErrorMessage = "Test")]
        public DateTime To { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Request Type")]
        public int RequestTypeId { get; set; }

        public string RequestType { get; set; }

        public bool IsApproved { get; set; }
    }
}
