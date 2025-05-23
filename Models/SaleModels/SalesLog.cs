namespace LabManagement.Models.SaleModels
{
    public class SalesLog
    {
        public int RecID { get; set; } = 0;
        public string PackageID { get; set;} = "";
        public string CaseNo { get; set;} = string.Empty;
        public DateTime ? TransDate { get; set; }=DateTime.Now;
        public bool Imported { get; set; } = false;
        public string UserID { get; set; } = string.Empty;
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;


    }
}
