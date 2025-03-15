namespace LabManagement.Models
{
    public class OutgoingModel
    {
        public DateTime? TransDate { get; set; } = DateTime.Now;
        public string DayName { get; set; } = "";
        public int TotalCases { get; set; } = 0;
        public int ShippedCases { get; set; } = 0;
    }
}
