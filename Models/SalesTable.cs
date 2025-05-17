namespace LabManagement.Models
{
    public class SalesTable
    {
        public int RecID { get; set; } = 0;        
        public string SalesID { get; set; } = "";
        public string SalesName { get; set; } = "";
        public string CaseNo { get; set; } = "";
        public string CaseRef { get; set; } = "";
        public DateTime? CaseDate { get; set; } =DateTime.Now;
        public DateTime? ShipDate { get; set; } = DateTime.Now;
        public string CustomerID { get; set; } = "";

        [ModelAttribute("NotTableField")]
        public string CustomerName { get; set; }
        public string DoctorName { get; set; } = "";
        public string PatientName { get; set; } = "";
        public string WorkTicketNotes { get; set; } = "";
        public string TranslateNotes { get; set; } = "";
        public int SalesStatus { get; set; } = 0;
        public string DocAccount { get; set; } = "";
        public string PaymentMode { get; set; } = "";
        public DateTime? ShippingDateRequested { get; set; } = DateTime.Now;
        public DateTime? ShippingDateConfirmed { get; set; } = DateTime.Now;
        public DateTime? CustReqShipDate { get; set; } = DateTime.Now;
        public string LabpanNum { get; set; } = "";
        public string BoxRef { get; set; } = "";
        public string Assignment { get; set; } = "";

        [ModelAttribute("NotTableField")]
        public string SalesStatusName { get; set; } = "";
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; } = DateTime.Now;
        public string UserID { get; set; } = "";
        public string DATAAREAID { get; set; } = "";

        [ModelAttribute("NotTableField")]
        public string LabName { get; set; } = "";

        [ModelAttribute("NotTableField")]
        public bool Selected { get; set; } = false;
        public bool ShowImages { get; set; } = true;
        public bool ShowFiles { get; set; } = true;
        public bool ReadOnly { get; set; } = false;
        public bool FocusedRow { get; set; } = false;
        
        public string Image64 { get; set; } = "";
    }
}
