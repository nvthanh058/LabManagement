namespace LabManagement.Models.ProductionModels
{
    public class LocationModel
    {
        public DateTime? TransDate { get; set; } = DateTime.Now;
        public string LineName { get; set; } = "";
        public int TotalCases { get; set; } = 0;
        public string LocationColor { get; set; } = "";
    }
}
