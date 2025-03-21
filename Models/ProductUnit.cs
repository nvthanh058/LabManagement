namespace LabManagement.Models
{
    public class ProductUnit
    {
        public int RecID { get; set; } = 0;
        public string UnitID { get; set; } = "";
        public string UnitEn { get; set; } = "";
        public string UnitVn { get; set; } = "";
        public string DATAAREAID { get; set; } = "";
        public bool IsUpdate { get; set; } = false;
        public bool Selected { get; set; } = false;

    }
}
