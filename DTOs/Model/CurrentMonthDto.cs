namespace DTOs.Models
{
    public class CurrentMonthDto
    {
        public string Month { get; set; }

        public int Days { get; set; }

        public string FirstDayOfMonth { get; set; }

        public byte[,] Dates { get; set; }
    }
}
