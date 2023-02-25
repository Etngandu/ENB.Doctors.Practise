using ENB.Doctors.Practise.Entities;

namespace ENB.Doctors.Practise.MVC.Models
{
    public class DisplayPatientRecord
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }

        public int PatientId { get; set; }

        public Staff? Staff { get; set; }

        public int? StaffId { get; set; }
        public string NamePatient { get; set; } = string.Empty;

        public string NameStaff { get; set; } = string.Empty;

        public Component_Code Component_Code { get; set; }
       
        public string Other_Details { get; set; } = string.Empty;
        
        public DateTime DateCreated { get; set; }
        
        public DateTime DateModified { get; set; }


    }
}
