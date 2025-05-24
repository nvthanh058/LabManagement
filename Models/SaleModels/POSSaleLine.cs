namespace LabManagement.Models.SaleModels
{
    public class POSSaleLine
    {
        public int RecID { get; set; } = 0;
        public string OrderID { get; set; } = "";
        public string TransID { get; set; } = "";
        public string ItemID { get; set; } = "";
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";

        public Double SalesQty { get; set; } = 1;
        public string SalesUnit { get; set; } = "";
        public Double SalesPrice { get; set; } = 0;
        public Double LineAmount
        {
            get
            {
                return Math.Round(SalesQty * SalesPrice,0);
            }
            set
            {
                LineAmount = value;
            }
        }

        public Double DiscountPercent { get; set; } = 0;
        public Double DiscountAmount { get; set; } = 0;
        public Double TotalAmount
        {
            get
            {
                return (LineAmount - DiscountAmount);
            }
            set
            {
                TotalAmount = value;
            }
        }

        public bool IsUpdate { get; set; } = false;

    }
}
