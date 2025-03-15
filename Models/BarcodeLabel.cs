namespace LabManagement.Models
{
    public class BarcodeLabel
    {
        public int? BarcodeType { get; set; } = 1;
        public string BarcodeContent { get; set; } = "";        
        public string Image64 { get; set; }=""; 
    }
}
