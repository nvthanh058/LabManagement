namespace LabManagement.Models
{
    public class CaseTracking
    {
        public int RecID { get; set; } = 0;
        public string TransID { get; set; } = "";
        public string SalesID { get; set; } = "";
        public string CaseNo { get; set; } = "";
        public string LabNum { get; set; } = "";
        public string PatientName { get; set; } = "";
        public DateTime? TransDate { get; set; } = DateTime.Now;
        public string RushCategory { get; set; } = "";
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; } = DateTime.Now;
        public string UserID { get; set; } = "";
        public string UpdateUpdatedUser { get; set; } = "";
        public string LabID { get; set; } = "";
        public bool Selected { get; set; } = false;
        public bool FocusedRow { get; set; } = false;
        public string ConcernIssue { get; set; } = "";
        public string TechnicianSuggestion { get; set; } = "";
        public string Response { get; set; } = "";
        public DateTime? ResponseDate { get; set; } = DateTime.Now;

        public string LabStatus { get; set; } = "";
        public string FactoryStatus { get; set; } = "";

    }
}
