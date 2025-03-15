using System.ComponentModel.DataAnnotations;

namespace LabManagement.Models
{
    public class UserGroupInfo
    {
        public int RecID { get; set; } = 0;

        public string GroupID { get; set; } = "";

        [Required]
        [MaxLength(250)]
        public string GroupName { get; set; } = "";
              
       
    }
}
