namespace LabManagement.Models.SaleModels
{
    public class SalesLine
    {
        public int RecID { get; set; } = 0;
        public string SalesID { get; set; } = "";        
        public string CaseNo { get; set; } = "";
        public int SalesStatus { get; set; } = 0;
        public string PatientName { get; set; } = "";
        public string ItemID { get; set; } = "";
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string UsTeeth { get; set; } = "";
        public string EurTeeth { get; set; } = "";
        public string Shade { get; set; } = "";
        public double SalesQty { get; set; } = 1;
        public double SalesPrice { get; set; } = 0;
        public double LineAmount { get; set; } = 0;
        public string CurrencyCode { get; set; } = "";
        public string OtherNotes { get; set; } = "";

        [Model("NotTableField")]
        public string CustomerRequests { get; set; } = "";

        [Model("NotTableField")]
        public string WorkNotes { get; set; } = "";

        public string Rework { get; set; } = "";
        public string RemakeCode { get; set; } = "";
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; } = DateTime.Now;
        public double QtyOrdered { get; set; } = 1;
        public string INVENTTRANSID { get; set; } = "";
        public string INVENTREFTRANSID { get; set; } = "";

        [Model("NotTableField")]
        public string UserID { get; set; } = "";
        public string DATAAREAID { get; set; } = "";

        [Model("NotTableField")]
        public bool Selected { get; set; } = false;
    }
}
