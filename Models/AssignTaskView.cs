namespace LabManagement.Models
{
    public class AssignTaskView
    {
        public UserInfo CurrentUser { get; set; } = new();
        public List<ProductionTask> ProductionTasks=new List<ProductionTask>();

    }
}
