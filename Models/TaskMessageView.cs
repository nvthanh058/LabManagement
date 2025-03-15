namespace LabManagement.Models
{
    public class TaskMessageView
    {
        public UserInfo CurrentUser { get; set; } = new();
        public IQueryable<TaskMessage>? TaskMessages;

    }
}
