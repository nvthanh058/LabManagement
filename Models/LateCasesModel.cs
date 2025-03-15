namespace LabManagement.Models
{
    public class LateCasesModel
    {
        public DateTime? TransDate { get; set; } = DateTime.Now;
        public string DayName { get; set; } = "";
        public int TotalCases { get; set; } = 0;
        public int LateCases { get; set; } = 0;
        
    }
}
