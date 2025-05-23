namespace LabManagement.Models.ProductionModels
{
    public class TaskMessage
    {
        public int RecID { get; set; } = 0;
        public string TransID { get; set; } = "";
        public string TaskID { get; set; } = "";
        public string FromUserID { get; set; } = "";
        [Model("NotTableField")]
        public string FromUserName { get; set; } = "";
        public string ToUserID { get; set; } = "";
        [Model("NotTableField")]
        public string ToUserName { get; set; } = "";

        public DateTime TransDate { get; set; } = DateTime.Now;
        public DateTime SendTime { get; set; } = DateTime.Now;
        public string MessageContent { get; set; } = "";
        public string FilePath { get; set; } = "";

        [Model("NotTableField")]             
        public int MessageStatus { get; set; } = 0;

        [Model("NotTableField")]
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
