using ENB.Doctors.Practise.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ENB.Doctors.Practise.MVC.Models
{
    public class DisplayAppointment
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }
        public int PatientId { get; set; }
       
        public Staff? Staff { get; set; }
        public int? StaffId { get; set; }
        
        public AppointmentStatus EventStatus { get; set; }

        [Display(Name = "Date of event")]
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? Color { get; set; }
        public Boolean AllDay { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string BookingNumber { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }
        
        public DateTime DateModified { get; set; }

        public string Other_details { get; set; } = string.Empty;

    }
}
