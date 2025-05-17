namespace LabManagement.Models
{
    public class CaseTrackingTask
    {
        public int RecID { get; set; } = 0;
        public string TransID { get; set; } = "";
        public string LineID { get; set; } = "";
        public string LineName { get; set; } = "";
        public DateTime? TransDate { get; set; } = DateTime.Now;
        public string Responsibility { get; set; } = "";
        public string LocationNotes { get; set; } ="";
        public string UserID { get; set; } = "";
        public string LineStatus { get; set; } = "";

    }
}
