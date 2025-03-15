namespace LabManagement.Models
{
    public class CaseOrderLine
    {
        public int RecID { get; set; } = 0;
        public string OrderID { get; set; } = "";
        public string TransID { get; set; } = "";
        public string ItemID { get; set; } = "";
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string UsTeeth { get; set; } = "";
        public string EurTeeth { get; set; } = "";
        public string Shade { get; set; } = "";
        public double Quantity { get; set; } = 1;     
        public string OtherNotes { get; set; } = "";
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; } = DateTime.Now;

        public string DATAAREAID { get; set; } = "";
    }
}
