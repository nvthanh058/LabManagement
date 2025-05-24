namespace LabManagement.Models.SaleModels
{
    public class POSSaleOrder
    {
        public int RecID { get; set; } = 0;
        public string SalesID { get; set; } = "";
        public string InvoiceID { get; set; } = DateTime.Now.ToString("yyMMddHHmmssfff");
        public DateTime? TransDate { get; set; } = DateTime.Now;
        public string CustomerID { get; set; } = "";

        public double TotalAmount {
            get
            {
                return lineItems==null ? 0 : lineItems.Sum(x=>x.TotalAmount);
            }
            set
            {
                TotalAmount = value;
            }
        }

        public double DiscountPercent { get; set; } = 0.0;

        public double DiscountAmount {
            get
            {
                return Math.Round(TotalAmount*DiscountPercent * 1.0 /100,0);
            }
            set
            {
                DiscountAmount = value;
            }
        }
        public double TotalCharge { get {
                return (TotalAmount - DiscountAmount);
            }
            set
            {
                TotalCharge = value;
            }
        }
        public string UserID { get; set; } = "";

        public List<POSSaleLine> lineItems { get; set; } = new List<POSSaleLine>();

    }

}
