using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LabManagement.Models
{
    public class UserInfo
    {
        public int RecID { get; set; } = 0;

        [Required]
        [MaxLength(20)]
        public string UserID { get; set; }

        [Required]
        [MaxLength(150)]

        public string UserName { get; set; } = "";
        public string EmplRefID { get; set; } = "";
        public string Password { get; set; } = "";

        [Required]
        [MaxLength(20)]
        public string GroupID { get; set; } = "";

        public int RoleID { get; set; } = 0;
        
        public string GroupName { get; set; } = "";

        public string PhotoUrl { get; set; } = "";

        public int Active { get; set; } = 1;

        [ModelAttribute("NotTableField")]
        public string GetPhotoUrl
        {
            get
            {
                return "images/" + PhotoUrl;
            }
        }
        [ModelAttribute("NotTableField")]

        public int TotalOrders { get; set; } = 0;

    }
}
