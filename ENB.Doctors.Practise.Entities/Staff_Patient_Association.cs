using ENB.Doctors.Practise.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Doctors.Practise.Entities
{
    public class Staff_Patient_Association : DomainEntity<int>,IDateTracking
    {
        #region Properties
        /// <summary>
        /// Gets or sets the owner (a Staff member) of the address.
        /// </summary>

        public Patient? Patient { get; set; }

        public int PatientId { get; set; }

        /// <summary>
        /// Gets or sets the owner address.
        /// </summary>

        public Staff? Staff { get; set; }

        public int? StaffId { get; set; }

        public DateTime Association_Start_Date { get; set; }

        public DateTime? Association_End_Date { get; set; }

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
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           if(Association_Start_Date> Association_End_Date)
            {
                yield return new ValidationResult("Start_Date should be come before End_Date ", new[] { "Association_End_Date", "Association_Start_Date" });
            }
        }
        #endregion
    }
}
