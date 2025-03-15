using System.ComponentModel.DataAnnotations;

namespace LabManagement.Models
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="You have to input UserName")]
        public string? UserName { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "You have to input Password")]
        public string? Password { get; set; } = "";
      
    }
}
