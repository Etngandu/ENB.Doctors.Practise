using ENB.Doctors.Practise.Entities.Collections;
using ENB.Doctors.Practise.Entities;
using FluentValidation;
using System.ComponentModel;

namespace ENB.Doctors.Practise.Blazor.Models
{
    public class CreateAndEditPatient
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

        public Address? PatientAddress { get; set; }

        public Appointments? Appointments { get; set; }

        public Staff_Patient_Associations? Staff_Patient_Associations { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{            
        //    if (Gender == Gender.None)
        //    {
        //        yield return new ValidationResult("Gender can't be None", new[] { "Gender" });
        //    }
        //}
    }
        public class CreateAndEditPatientValidator : AbstractValidator<CreateAndEditPatient>
        {
            public CreateAndEditPatientValidator()
            {
                RuleFor(x => x.Patient_first_name)
                .NotEmpty()
                .WithMessage("FirstName  can't be empty");

                RuleFor(x => x.Patient_last_name)
               .NotEmpty().WithMessage("LastName  can't be empty");

                RuleFor(x => x.Patient_mailAddress)
               .NotEmpty().WithMessage("Mail can't be empty")
               .EmailAddress();

                RuleFor(x => x.Gender)
               .NotEqual(Gender.None)
               .WithMessage("Gender  can't be None");

                RuleFor(x => x.Cell_Mobile_phone)
               .NotEmpty().WithMessage("PhoneNumber  can't be empty");

                RuleFor(x => x.Date_Of_Birth)
               .LessThan(x => DateTime.Now)
               .WithMessage($"DateOfBirth should be less than {DateTime.Now}");

            }

        
    }
}
