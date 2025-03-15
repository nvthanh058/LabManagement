namespace LabManagement.Models
{
    public class TaskMessage
    {
        public int RecID { get; set; } = 0;
        public string TransID { get; set; } = "";
        public string TaskID { get; set; } = "";
        public string FromUserID { get; set; } = "";
        [ModelAttribute("NotTableField")]
        public string FromUserName { get; set; } = "";
        public string ToUserID { get; set; } = "";
        [ModelAttribute("NotTableField")]
        public string ToUserName { get; set; } = "";

        public DateTime TransDate { get; set; } = DateTime.Now;
        public DateTime SendTime { get; set; } = DateTime.Now;
        public string MessageContent { get; set; } = "";
        public string FilePath { get; set; } = "";

        [ModelAttribute("NotTableField")]             
        public int MessageStatus { get; set; } = 0;

        [ModelAttribute("NotTableField")]
        public string PhotoUrl { get; set; } = "";

        public string GetPhotoUrl
        {
            get
            {
                return "images/" + PhotoUrl;
            }
        }

    }
}
