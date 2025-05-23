namespace LabManagement.Models.Commons
{
    public class ProductGroup
    {
        public int RecID { get; set; } = 0;
        public string GroupID { get; set; } = "";
        public string GroupName { get; set; } = "";

        public bool IsUpdate { get; set; } = false;
    }
}
