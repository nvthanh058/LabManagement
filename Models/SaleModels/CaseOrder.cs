namespace LabManagement.Models.SaleModels
{
    public class CaseOrder
    {
        public int RecID { get; set; } = 0;        
        public string OrderID { get; set; } = "";
        public string OrderNo { get; set; } = DateTime.Now.ToString("yyMMddHHmmssfff");     
        public DateTime? OrderDate { get; set; } =DateTime.Now;       
        public string PatientName { get; set; } = "";
        public string DoctorName { get; set; } = "";
        public string WorkNotes { get; set; } = "";
        
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; } = DateTime.Now;
        public string UserID { get; set; } = "";
        public string DATAAREAID { get; set; } = "";

        [Model("NotTableField")]
        public bool Selected { get; set; } = false;
        public bool ShowImages { get; set; } = true;
        public bool ShowFiles { get; set; } = true;
        public bool ReadOnly { get; set; } = false;
        public string DownloadStatus { get; set; } = "";

    }
}
