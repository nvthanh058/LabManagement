namespace LabManagement.Models
{
    public class FixedPayCode
    {
        public int RecID { get; set; }
        public string PayCode { get; set; } = "";
        public string EmplID { get; set; } = "";

        [ModelAttribute("NotTableField")]
        public string FullName { get; set; } = "";

        [ModelAttribute("NotTableField")]
        public string Description { get; set; } = "";
        
        public DateTime? FromDate { get; set; } = DateTime.Now;
        public DateTime? ToDate { get; set; } = DateTime.Now;
        public string CurrencyCode { get; set; } = "VND";
        public double AmountCur { get; set; } = 0;
        public bool FindNext { get; set; } = false;
    }
}
