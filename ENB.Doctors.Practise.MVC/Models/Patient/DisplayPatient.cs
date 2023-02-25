using ENB.Doctors.Practise.Entities.Collections;
using ENB.Doctors.Practise.Entities;
using System.ComponentModel;

namespace ENB.Doctors.Practise.MVC.Models
{
    public class DisplayPatient
    {
        public int Id { get; set; }

        [DisplayName("Patient first name ")]
        public string Patient_first_name { get; set; } = string.Empty;

        [DisplayName("Patient last name ")]
        public string Patient_last_name { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        [DisplayName("Patient mail Address ")]
        public string Patient_mailAddress { get; set; } = string.Empty;

        public string Height { get; set; } = string.Empty;

        [DisplayName("Hospital Number")]
        public string Hospital_Number { get; set; } = string.Empty;

        [DisplayName("NHS Number")]
        public string nhs_number { get; set; } = string.Empty;

        public Gender Gender { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime Date_Of_Birth { get; set; }
        public string Weight { get; set; } = string.Empty;

        [DisplayName("Next of Kin")]
        public string Next_of_kin { get; set; } = string.Empty;

        [DisplayName("Phone Number")]
        public string Cell_Mobile_phone { get; set; } = string.Empty;
        
        [DisplayName("Other Details")]
        public string Other_patient_details { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Address? PatientAddress { get; set; }

        public Appointments? Appointments { get; set; }

        public Staff_Patient_Associations? Staff_Patient_Associations { get; set; }
    }
}
