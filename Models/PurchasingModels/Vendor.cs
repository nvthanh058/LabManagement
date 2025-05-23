namespace LabManagement.Models.PurchasingModels
{
    public class Vendor
    {
        public int RecID { get; set; } = 0;
        public string VendorID { get; set; } = "";
        public string VendorName { get; set; } = "";
        public string GroupID { get; set; } = "";
        public string Address { get; set; } = "";
        public string Attention { get; set; } = "";
        public string Tel { get; set; } = "";
        public string MobilePhone { get; set; } = "";
        public string Email { get; set; } = "";
        public string PaymentTerm { get; set; } = "";
        public string TradingTerm { get; set; } = "";
        public string CurrencyCode { get; set; } = "";
        public string LabID { get; set; } = "";
        public DateTime ? CreatedDate { get; set; } =DateTime.Now;
        public string UserID { get; set; } = "";
        public string Remarks { get; set; } = "";

        public bool Selected { get; set; } = false;


    }
}
