namespace LabManagement.Models
{
    public class TablePriceMaster
    {
        public int RecID { get; set; } = 0;
        public string PriceID { get; set; } = String.Empty;
        public string PriceName { get; set; } = String.Empty;
        [ModelAttribute("NotTableField")]
        public bool Selected { get; set; } = false;
    }
}
