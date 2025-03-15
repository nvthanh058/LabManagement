namespace LabManagement.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ModelAttribute : Attribute
    {
        public string Description { get; }
        public ModelAttribute(string description)
        {
            Description = description;
        }
    }

   
}
