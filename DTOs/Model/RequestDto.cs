using DTOs.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Models
{
    public class RequestDto : BaseDto
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DateCompare]
        public DateTime From { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DateCompare]
        public DateTime To { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Request Type")]
        public int RequestTypeId { get; set; }

        public string RequestType { get; set; }

        public bool IsApproved { get; set; }

        public string User { get; set; }
    }
}
