namespace LabManagement.Models
{
    public class ReportCasesInfo
    {
        public int RecID { get; set; } = 0;
        public string Invoice { get; set; } = "";
        public string Company { get; set; } = "";
        public string Doctor { get; set; } = "";
        public string Patient { get; set; } = "";
        public string Pan { get; set; } = "";
        public string Products { get; set; } = "";
        public int Units { get; set; } = 1;
        public string OrderedDate { get; set; } = "";
        public string DueDate { get; set; } = "";
        public string Appt { get; set; } = "";
        public string Dept { get; set; } = "";
        public string UserID { get; set; } = "";
        public string Status { get; set; } = "";

    }
}
