namespace DTOs.ViewModels
{
    public class BaseViewModel
    {
        public int Id { get; set; }

        public virtual string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
