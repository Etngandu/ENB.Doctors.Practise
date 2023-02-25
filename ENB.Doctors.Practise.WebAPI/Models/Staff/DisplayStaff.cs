using ENB.Doctors.Practise.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ENB.Doctors.Practise.WebAPI.Models
{
    public class DisplayStaff
    {
        public int Id { get; set; }

        [DisplayName("Staff Category code")]
        public Staff_Category_Code Staff_Category_code { get; set; }    

        public Gender Gender { get; set; }

        [DisplayName("Staff Job Title")]
        public Staff_JobTitle Staff_job_title { get; set; }

        [DisplayName("First Name")]
        public string Staff_first_name { get; set; } = string.Empty;

        [DisplayName("Last Name")]
        public string Staff_last_name { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string EmailAddressText { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        [DisplayName("Date of birth")]
        public DateTime Staff_birth_date {  get; set; }

       
        public Address? StaffAddress { get; set; }

        [DisplayName("Other Details")]
        public string Other_staff_details { get; set; } = string.Empty;       

        
        public DateTime DateCreated { get; set; }
        
        public DateTime DateModified { get; set; }

    }
}
