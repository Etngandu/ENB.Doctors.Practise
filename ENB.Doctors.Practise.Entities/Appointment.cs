using ENB.Doctors.Practise.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Doctors.Practise.Entities
{
    public class Appointment : DomainEntity<int>,IDateTracking
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

        /// <summary>
        /// Gets or sets the Booking status.
        /// </summary>
        /// 
        public AppointmentStatus EventStatus { get; set; }

        [Display(Name = "Date of event")]
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? Color { get; set; }
        public Boolean AllDay { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        /// 
        public DateTime DateModified { get; set; }

        public string Other_details { get; set; } = string.Empty;

        /// <summary>
        /// Gets the Number of the booking.
        /// </summary>
        public string BookingNumber
        {
            get
            {
                string temp = DateCreated.ToLongTimeString() ?? string.Empty;
                if (!string.IsNullOrEmpty(StaffId.ToString()) &&
                    !string.IsNullOrEmpty(PatientId.ToString()))
                {

                    if (temp.Length > 0)
                    {
                        temp += "-";
                    }
                    temp += StaffId.ToString() + "/" + PatientId.ToString();
                }
                return temp.Replace(":", "");
            }
        }


        #endregion
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EventStatus == AppointmentStatus.None)
            {
                yield return new ValidationResult("EventStatus can't be None.", new[] { "EventStatus" });
            }
        }
    }
}
