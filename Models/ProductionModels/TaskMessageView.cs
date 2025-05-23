namespace LabManagement.Models.ProductionModels
{
    public class TaskMessageView
    {
        public UserInfo CurrentUser { get; set; } = new();
        public IQueryable<TaskMessage>? TaskMessages;

    }
}
