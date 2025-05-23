namespace LabManagement.Models.ProductionModels
{
    public class ProductionTask
    {
        public int RecID { get; set; } = 0;        
        public string TaskID { get; set; } = "";
        public string TaskRefID { get; set; } = "";
        public DateTime? TransDate { get; set; } = DateTime.Now;        
        public string EmplRefID { get; set; } = "";

        [Model("NotTableField")]
        public string EmplID { get; set; } = "";
        [Model("NotTableField")]
        public string FullName { get; set; } = "";
        public string LineID { get; set; } = "";

        [Model("NotTableField")]
        public string LineName { get; set; } = "";
        
        [Model("NotTableField")]
        public string CaseNo { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string UsTeeth { get; set; } = "";
        public string Shade { get; set; } = "";
        public int Quantity { get; set; } = 1;
        public string Notes { get; set; } = "";        
        public int JobType { get; set; } = 0;

        [Model("NotTableField")]
        public string JobTypeName { get; set; } = "New";

        [Model("NotTableField")]
        public int ProdStatus { get; set; } = 0;
     
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public string UserRefID { get; set; } = "";

        [Model("NotTableField")]
        public string PhotoUrl { get; set; } = "";
        [Model("NotTableField")]
        public string GetPhotoUrl
        {
            get
            {
                return "images/" + PhotoUrl;
            }
        }

        public string DisplayName
        {
            get
            {
                if (EmplID != "")
                {
                    return "(" + EmplID + ") " + FullName;
                }
                else
                {
                    return "";
                }
                
            }
        }

        public string DATAAREAID { get; set; } = "";

    }
}
