namespace LabManagement.Models
{
    public class DueIn2DaysCases
    {
        public DateTime? TransDate { get; set; } = DateTime.Now;
        public string Category { get; set; } = "";
        public int DueDaysCases { get; set; } = 0;
        public int TotalCases { get; set; } = 0;
    }
}
