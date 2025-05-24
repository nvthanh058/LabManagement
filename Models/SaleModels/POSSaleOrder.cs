namespace LabManagement.Models.SaleModels
{
    public class POSSaleOrder
    {
        public int RecID { get; set; } = 0;
        public string SalesID { get; set; } = "";
        public string InvoiceID { get; set; } = DateTime.Now.ToString("yyMMddHHmmssfff");
        public DateTime? TransDate { get; set; } = DateTime.Now;
        public string CustomerID { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string Phone { get; set; } = "";
        public double TotalAmount {get; set; } = 0;
        public double DiscountPercent { get; set; } = 0.0;

        public double DiscountAmount{  get; set; } = 0.0;
        public double TotalCharge {  get; set; } = 0.0;
        public string UserID { get; set; } = "";

        public bool Selected { get; set; } = false;  

    }

}
