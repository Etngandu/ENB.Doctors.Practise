using ENB.Doctors.Practise.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ENB.Doctors.Practise.MVC.Models
{
    public class CreateAndEditStaff_Patient_Association: IValidatableObject
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }

        public int PatientId { get; set; }

        public Staff? Staff { get; set; }

        public int? StaffId { get; set; }

        public DateTime Association_Start_Date { get; set; }

        public DateTime? Association_End_Date { get; set; }

        public string Other_Details { get; set; } = string.Empty;

        public List<SelectListItem>? ListStaff { get; set; }

        public List<SelectListItem>? ListPatient { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           if (StaffId==0)
            {
                yield return new ValidationResult("Staff can't be none", new[] { "Staff" });
            }

            if (PatientId == 0)
            {
                yield return new ValidationResult("Patient can't be none", new[] { "PatientId" });
            }
        }
    }
}
