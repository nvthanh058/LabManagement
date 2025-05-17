namespace LabManagement.Models.HRMS
{
    public class AttPeriod
    {
        public int RecID { get; set; } = 0;
        public string PeriodID { get; set;} = DateTime.Now.ToString("MMMyyyy");
        public DateTime ? FromDate { get; set; }  = DateTime.Now;
        public DateTime? ToDate { get; set; } = DateTime.Now;
        public bool Locked { get; set; } = false;

    }
}
