namespace LabManagement.Models
{
    public class PayCodeInfo
    {
        public int RecID { get; set; }
        public string PayCode { get; set; } = "";
        public string Description { get; set; } = "";
        public int PayType { get; set; } = 0;
     
        [ModelAttribute("NotTableField")]
        public string PayTypeName { get; set; } = "";
        public string LedgerAccount { get; set; } = "";
        public bool Nontaxable { get; set; } = false;
        public int PayCodeUnit { get; set; } = 0;
        [ModelAttribute("NotTableField")]
        public string Units { get; set; } = "Months";
        public bool NonCashAllowance { get; set; } = false;
        public int FixedOrAttendanceBase { get; set; } = 0;
        public bool IncludeInCost { get; set; } = false;
        public string OffsetLedgerAccount { get; set; } = "";
        public bool NettCalculation { get; set; } = false;
        public bool IfsIntegration { get; set; } = false;
        public int AttendanceDays { get; set; } = 0;
        public int TaxablePercent { get; set; } = 0;
        public int NontaxablePercent { get; set; } = 0;
    }
}
