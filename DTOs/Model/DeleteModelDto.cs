namespace DTOs.Models
{
    public class DeleteModelDto
    {
        public int Id { get; set; }

        public string ButtonName { get; set; } = "Delete";

        public string ControllerName { get; set; }

        public bool IsEnabled { get; set; }
    }
}
