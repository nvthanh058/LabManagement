namespace LabManagement.Models.ProductionModels
{
    public class CaseResponse
    {
        public int RecID { get; set; } = 0;        
        public string TransRefID { get; set; } = "";
        public string Response { get; set; } = "";        
        public DateTime? ResponseDate { get; set; } = DateTime.Now;
        public string LabStatus { get; set; } = "";
        public string UserID { get; set; } = "";

    }
}
