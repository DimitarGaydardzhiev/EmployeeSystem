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

        public string Description { get; set; }

        [Required]
        public int Type { get; set; }
    }
}
