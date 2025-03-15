namespace LabManagement.Models
{
    public class TablePrice
    {
        public int RecID { get; set; } = 0;
        public string PriceID { get; set; } = String.Empty;
        public string ItemID { get; set; } = String.Empty;
        [ModelAttribute("NotTableField")]
        public string ItemCode { get; set; } = String.Empty;
        [ModelAttribute("NotTableField")]
        public string ItemName { get; set; } = String.Empty;
        public double UnitPrice { get; set; } = 0;
        public DateTime? FromDate { get; set; } = DateTime.Now;
        public string UserRef { get; set; } = String.Empty;
        public string DATAAREAID { get; set; } = String.Empty;

        [ModelAttribute("NotTableField")]
        public bool Selected { get; set; } = false;

    }
}
