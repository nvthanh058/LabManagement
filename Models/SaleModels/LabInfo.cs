namespace LabManagement.Models.SaleModels
{
    public class LabInfo
    {
        public int RecID { get; set; } = 0;
        public string DATAAREAID { get; set; } = "";
        public string LabName { get; set; } = "";
        public string LabAddress { get; set; } = "";
        public string Tel { get; set; } = "";
        public string Website { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsUpdate { get; set; } = false;

    }
}
