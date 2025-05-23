﻿namespace LabManagement.Models.SaleModels
{
    public class Customer
    {
        public int RecID { get; set; } = 0;

        [Model("NotTableField")]
        public string CustomerID { get; set; } = "";
        public string CustomerCode { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string NameAlias { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string EmailAddress { get; set; } = "";
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; } = DateTime.Now;
        public string DATAAREAID { get; set; } = "";

        [Model("NotTableField")]
        public bool Selected { get; set; } = false;

    }
}
