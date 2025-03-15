namespace LabManagement.Models
{
    public class ProductLine
    {

        public int RecID { get; set; } = 0;

        [ModelAttribute("NotTableField")]
        public string LineID { get; set; } = "";
        public string LineName { get; set; } = "";
        public int OrderNo { get; set; } = 0;
        public string LocationGroup { get; set; } = "CB";
        public string LocationColor { get; set; } = "#33ceff";
        public string DATAAREAID { get; set; } = "";
        
    }
}
