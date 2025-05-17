namespace LabManagement.Models
{
    public class SequenceInfo
    {
        public int StartNum { get; set; } = 1;
        public string Format { get; set; } = "";
        public string SeqNumber { get {
                return "" + StartNum;
            } 
        }
    }
}
