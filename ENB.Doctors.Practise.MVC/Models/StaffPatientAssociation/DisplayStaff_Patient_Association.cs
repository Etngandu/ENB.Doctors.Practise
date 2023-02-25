using ENB.Doctors.Practise.Entities;
using System.ComponentModel;

namespace ENB.Doctors.Practise.MVC.Models
{
    public class DisplayStaff_Patient_Association
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }

        public int PatientId { get; set; }      

        public Staff? Staff { get; set; }

        public int? StaffId { get; set; }

        public string NamePatient { get; set; } = string.Empty;

        public string NameStaff { get; set; } = string.Empty;

        [DisplayName("Association Start Date")]
        public DateTime Association_Start_Date { get; set; }

        [DisplayName("Association End Date")]
        public DateTime? Association_End_Date { get; set; }       

        public string Other_Details { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }
        
        public DateTime DateModified { get; set; }
    }
}
