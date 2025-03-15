namespace LabManagement.Models
{
    public class OrderFile
    {
        public int RecID { get; set; } = 0;      
        public string OrderID { get; set; } = "";
        public string FilePath { get; set; } = "";
        public int FileType { get; set; } = 0;

        public string UserID { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.Now;

    }
}
