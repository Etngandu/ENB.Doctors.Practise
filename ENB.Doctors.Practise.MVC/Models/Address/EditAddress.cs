using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ENB.Doctors.Practise.Entities;
using ENB.Doctors.Practise.MVC.Models;

namespace ENB.Doctors.Practise.MVC.Models
{
    public class EditAddress : IValidatableObject
    {
        public int PatientId { get; set; }
        public int StaffId { get; set; }       
        public string? Number_street { get; set; }
        public string? City { get; set; }
        public string? Zipcode { get; set; }
        public string? State_province_county { get; set; }
        public string? Country { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new Address(Number_street!, City!, Zipcode!, State_province_county!, Country!).Validate();
        }
    }
}