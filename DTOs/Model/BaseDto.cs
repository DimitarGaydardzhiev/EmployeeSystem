namespace DTOs.Models
{
    public class BaseDto
    {
        public int Id { get; set; }

        public virtual string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
