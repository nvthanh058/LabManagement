using System.ComponentModel.DataAnnotations;

namespace LabManagement.Models
{
    public class ChangePassModel
    {
        
        public UserInfo? CurrentUser { get; set; }

        
        public string? OldPassword { get; set; } = "";
        
        public string? NewPassword { get; set; } = "";
        
        public string? ConfirmPassword { get; set; } = "";

        
    }
}
