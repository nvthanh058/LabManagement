namespace LabManagement.Models
{
    public class ProdTable
    {
        public int RecID { get; set; } = 0;

        [ModelAttribute("NotTableField")]
        public DateTime? TransDate { get; set; } = DateTime.Now;
        public string ItemID { get; set; } = "";
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";
        public int ProdStatus { get; set; } = 0;

        public double Quantity { get; set; } = 1;
        [ModelAttribute("NotTableField")]
        public string INVENTTRANSID { get; set; } = "";
        [ModelAttribute("NotTableField")]
        public string INVENTREFTRANSID { get; set; } = "";
        [ModelAttribute("NotTableField")]
        public string SalesID { get; set; } = "";
        public string CaseNo { get; set; } = "";

        [ModelAttribute("NotTableField")]
        public DateTime? ShipDate { get; set; } = DateTime.Now;

        [ModelAttribute("NotTableField")]
        public string PatientName { get; set; } = "";
        [ModelAttribute("NotTableField")]
        public string DoctorName { get; set; } = "";
        [ModelAttribute("NotTableField")]
        public string LabpanNum { get; set; } = "";
        public string CustomerRequests { get; set; } = "";
        public string WTNotes { get; set; } = "";
        public string UsTeeth { get; set; } = "";
        public string EurTeeth { get; set; } = "";
        public string Shade { get; set; } = "";
        public string Assignment { get; set; } = "";

        [ModelAttribute("NotTableField")]
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
        [ModelAttribute("NotTableField")]
        public DateTime? ModifiedDateTime { get; set; } = DateTime.Now;        
        public string UserID { get; set; } = "";        
        public string DATAAREAID { get; set; } = "";

        [ModelAttribute("NotTableField")]
        public bool Selected { get; set; } = false;
        public bool FocusedRow { get; set; } = false;
        public bool ReadOnly { get; set; } = false;
    }
}
