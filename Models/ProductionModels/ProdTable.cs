namespace LabManagement.Models.ProductionModels
{
    public class ProdTable
    {
        public int RecID { get; set; } = 0;

        [Model("NotTableField")]
        public DateTime? TransDate { get; set; } = DateTime.Now;
        public string ItemID { get; set; } = "";
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";
        public int ProdStatus { get; set; } = 0;

        public double Quantity { get; set; } = 1;
        [Model("NotTableField")]
        public string INVENTTRANSID { get; set; } = "";
        [Model("NotTableField")]
        public string INVENTREFTRANSID { get; set; } = "";
        [Model("NotTableField")]
        public string SalesID { get; set; } = "";
        public string CaseNo { get; set; } = "";

        [Model("NotTableField")]
        public DateTime? ShipDate { get; set; } = DateTime.Now;

        [Model("NotTableField")]
        public string PatientName { get; set; } = "";
        [Model("NotTableField")]
        public string DoctorName { get; set; } = "";
        [Model("NotTableField")]
        public string LabpanNum { get; set; } = "";
        public string CustomerRequests { get; set; } = "";
        public string WTNotes { get; set; } = "";
        public string UsTeeth { get; set; } = "";
        public string EurTeeth { get; set; } = "";
        public string Shade { get; set; } = "";
        public string Assignment { get; set; } = "";

        [Model("NotTableField")]
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
        [Model("NotTableField")]
        public DateTime? ModifiedDateTime { get; set; } = DateTime.Now;        
        public string UserID { get; set; } = "";        
        public string DATAAREAID { get; set; } = "";

        [Model("NotTableField")]
        public bool Selected { get; set; } = false;
        public bool FocusedRow { get; set; } = false;
        public bool ReadOnly { get; set; } = false;
    }
}
