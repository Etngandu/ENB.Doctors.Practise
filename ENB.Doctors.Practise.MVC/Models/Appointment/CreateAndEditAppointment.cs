using ENB.Doctors.Practise.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ENB.Doctors.Practise.MVC.Models
{
    public class CreateAndEditAppointment :IValidatableObject
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }
        public int PatientId { get; set; }
        public Staff? Staff { get; set; }
        public int? StaffId { get; set; }
        public AppointmentStatus EventStatus { get; set;}

        [Display(Name = "Date of event")]
        public DateTime Start { get; set;}
        public DateTime? End { get; set;}
        public string? Color { get; set; }
        public Boolean AllDay { get; set; }        

        public string Other_details { get; set; } = string.Empty;

        public List<SelectListItem>? ListPatient { get; set; }

        public List<SelectListItem>? ListStaff { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(EventStatus==AppointmentStatus.None)
            {
                yield return new ValidationResult("EventStatus can't be none", new[] { "EventStatus" });

            }
        }
    }
}
