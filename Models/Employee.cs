using System.ComponentModel;

namespace LabManagement.Models
{
   
    public class Employee
    {
        public int RecID { get; set; } = 0;

        [ModelAttribute("NotTableField")]
        public string EmplRefID { get; set; }
        public string EmplID { get; set; } = "";
        public string FullName { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string Position { get; set; } = "";
        public string DeptID { get; set; } = "";
       
        [ModelAttribute("NotTableField")]
        public string DeptName { get; set; } = "";        
        public int ForeignLocal { get; set; } = 0;
        public DateTime? StartedDate { get; set; } =DateTime.Now;
        public DateTime? DOB { get; set; } = DateTime.Now;
        public string PlaceOfBirth { get; set; } = "";
        public string NativeCountry { get; set; } = "";
        public string NationalRegion { get; set; } = "";
        public string Gender { get; set; } = "";
        public string PassportNo { get; set; } = "";
        public DateTime? IssuedDate { get; set; } = null;
        public string IssuedPlace { get; set; } = "";
        public string PermanentAddress { get; set; } = "";
        public string TemporaryAddress { get; set; } = "";
        public string Education { get; set; } = "";
        public int WorkingDayWeek { get; set; } = 6;
        public int TotalHoursPeriod { get; set; } = 208;

        [ModelAttribute("NotTableField")]
        public bool Selected { get; set; } = false;
        public string PhotoUrl { get; set; } = "no-picture.png";

        [ModelAttribute("NotTableField")]
        public IFormFile ProfilePhoto { get; set; }

        [ModelAttribute("NotTableField")]
        public string GetPhotoUrl { get {
                return "images/" + PhotoUrl;
            } }

        public string DATAAREAID { get; set; } = "";

    }
}
