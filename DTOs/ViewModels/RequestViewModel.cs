using System;
using System.ComponentModel.DataAnnotations;

namespace DTOs.ViewModels
{
    public class RequestViewModel : BaseViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [Required]
        //[Compare("From", ErrorMessage = "Test")]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Request Type")]
        public int RequestTypeId { get; set; }

        public string RequestType { get; set; }

        public string IsApproved { get; set; }

        public string User { get; set; }
    }
}
