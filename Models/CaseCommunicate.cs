namespace LabManagement.Models
{
    public class CaseCommunicate
    {
        public int RecID { get; set; } = 0;
        public string SalesID { get; set; } = "";
        public string TransID { get; set; } = "";
        public string ConcernIssue { get; set; } = "";
        public string TechnicianSuggestion { get; set; } = "";
        public string Response { get; set; } = "";
        public DateTime? ResponseDate { get; set; } = DateTime.Now;
        public string LabStatus { get; set; } = "";
        public string FactoryStatus { get; set; } = "";
        public string UserID { get; set; } = "";


    }
}
