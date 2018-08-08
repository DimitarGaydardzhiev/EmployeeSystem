using System.ComponentModel.DataAnnotations;

namespace DTOs.InputModels
{
    public class PositionInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
