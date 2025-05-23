namespace LabManagement.Models.SaleModels
{
    public class IncomingModel
    {
        public DateTime? TransDate { get; set; } = DateTime.Now;
        public string DayName { get; set; } = "";
        public int TotalCases { get; set; } = 0;
    }
}
