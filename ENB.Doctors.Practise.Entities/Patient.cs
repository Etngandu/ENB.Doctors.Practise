using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ENB.Doctors.Practise.Entities.Collections;
using ENB.Doctors.Practise.Infrastructure; 
using ENB.Doctors.Practise.Entities;


namespace ENB.Doctors.Practise.Entities
{
    public class Patient : DomainEntity<int>, IDateTracking
    {

          #region Constructors

        ///// <summary>
        ///// Initializes a new instance of the Person class.
        ///// </summary>
        public Patient()
        {            
            PatientAddress = new Address();
            Appointments= new Appointments();   
            Staff_Patient_Associations= new Staff_Patient_Associations();
            Patient_Records= new Patient_Records(); 
        }

        #endregion


        #region "Public Properties"



        /// <summary>
        /// Gets or sets the first name of this person.
        /// </summary>
     
        public string Patient_first_name { get; set; } = string.Empty;     

        /// <summary>
        /// Gets or sets Patient_last_name.
        /// </summary>
        
        public string Patient_last_name { get; set; }=string.Empty;

        public string Patient_mailAddress { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets the fheight of this patient.
        /// </summary>

        public string Height { get; set; } = string.Empty;        

        /// <summary>
        /// Gets or sets the last name of this person.
        /// </summary>
       
        public string Hospital_Number { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the first name of this person.
        /// </summary>

        public string nhs_number { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of this person.
        /// </summary>

        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the PublicatieDatum of the Gastenboek.
        /// </summary>
        public DateTime Date_Of_Birth
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the weight of this patient.
        /// </summary>
        
        public string Weight { get; set; } = string.Empty;

        /// <summary>
        /// Gets or set Next of kin of this patient.
        /// </summary>

        public string Next_of_kin { get; set; } = string.Empty;

        

        /// <summary>
        /// Gets or sets the Cell_Mobile_phone of this patient.
        /// </summary>

        public string Cell_Mobile_phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Other_patient_details.
        /// </summary>

        public string Other_patient_details { get; set; } = string.Empty;


        /// <summary>
        /// Gets the full name of this patient.
        /// </summary>
        public string FullName
        {
            get
            {
                string temp = Patient_first_name ?? string.Empty;
                
                    if (!string.IsNullOrEmpty(Patient_last_name))
                    {
                        if (temp.Length > 0)
                        {
                            temp += " ";
                        }
                        temp += Patient_last_name;
                    }
                    else
                    {

                        temp += Patient_last_name;
                    }
                
                return temp;
            }
        }


        /// <summary>
        /// Gets or sets the date and time the object was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        public DateTime DateModified { get; set; }


        /// <summary>
        /// Gets or sets one to many relationship between Patient and addres
        /// </summary>

        public Address PatientAddress { get; set; }

        /// <summary>
        /// Gets or sets one to many relationship between Patient and Appointments
        /// </summary>

        public Appointments Appointments { get; set; }

        /// <summary>
        /// Gets or sets one to many relationship between Patient and Staff_Patient_Associations
        /// </summary>

        public Staff_Patient_Associations Staff_Patient_Associations { get; set; }

        /// <summary>
        /// Gets or sets one to many relationship between Patient and Patient_Records
        /// </summary>

        public Patient_Records Patient_Records { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (Type == PersonType.None)
            //{
            //    yield return new ValidationResult("Type can't be None.", new[] { "Type" });
            //}

            if (Date_Of_Birth < DateTime.Now.AddYears(Constants.MaxAgePerson * -1))
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }
            if (Date_Of_Birth > DateTime.Now)
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }

            foreach (var result in Appointments.Validate())
            {
                yield return result;
            }

            foreach (var result in Staff_Patient_Associations.Validate())
            {
                yield return result;
            }

           
        }
        #endregion

    }
}
