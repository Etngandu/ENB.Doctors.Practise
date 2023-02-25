using ENB.Doctors.Practise.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Doctors.Practise.Entities
{
    /// <summary>
    /// Represents Patient_Records in the system.
    /// </summary>

    public class Patient_Record : DomainEntity<int>, IDateTracking
    {

        #region Properties
        
        
        public Patient? Patient { get; set; }

        public int PatientId { get; set; }

        

        public Staff? Staff { get; set; }

        public int? StaffId { get; set; }


        /// <summary>
        /// Gets or sets the Component_Code of the patient.
        /// </summary>

        public Component_Code Component_Code { get; set; } 

        /// <summary>
        /// Gets or sets the Staff member middle name.
        /// </summary>

        public string Other_Details { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets the date and time the object was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        public DateTime DateModified { get; set; }


        #endregion

        #region Methods

        /// <summary>
        /// Validates this object. 
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Component_Code==Component_Code.None)
            {
                yield return new ValidationResult("Component_Code can't be none", new[] { "Component_Code" });
            }
            

        }
        #endregion

    }
}
