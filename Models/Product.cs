namespace LabManagement.Models
{
    public class Product
    {
        public int RecID { get; set; } = 0;
        public string ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string NameAlias { get; set; }
        public string ItemGroupID { get; set; }

        [ModelAttribute("NotTableField")]
        public string GroupName { get; set; }
        public string ItemType { get; set; }

        [ModelAttribute("NotTableField")]
        public string ItemTypeName { get; set; }

        public string UnitID { get; set; }
        public string PackagingGroup { get; set; }
        public string DimGroupID { get; set; }
        public string MaterialName { get; set; }
        public string BomCode { get; set; }
        public string BomItemName { get; set; }
        public double OnHand { get; set; } = 0;
        public double UnitPrice { get; set; } = 0;
        public double SalesPrice { get; set; } = 0;
        public string ProductImage { get; set; } = "";
        public string DATAAREAID { get; set; }
        [ModelAttribute("NotTableField")]
        public bool Selected { get; set; } = false;

        [ModelAttribute("NotTableField")]
        public bool IsUpdate { get; set; } = false;

    }
}
