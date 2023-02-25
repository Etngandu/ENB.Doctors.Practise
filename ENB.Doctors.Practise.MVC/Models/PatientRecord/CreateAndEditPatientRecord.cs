using ENB.Doctors.Practise.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ENB.Doctors.Practise.MVC.Models
{
    public class CreateAndEditPatientRecord:IValidatableObject
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }

        public int PatientId { get; set; }

        public Staff? Staff { get; set; }

        public int? StaffId { get; set; }

        public Component_Code Component_Code { get; set; }

        public string Other_Details { get; set; } = string.Empty;
        public List<SelectListItem>? ListStaff { get; set; }

        public List<SelectListItem>? ListPatient { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           if(Component_Code==Component_Code.None)
            {
                yield return new ValidationResult("Component_Code can't be None", new[] { "Component_Code" });
            }
        }
    }
}
