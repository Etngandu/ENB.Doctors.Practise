using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Doctors.Practise.Entities
{
    /// <summary>
    /// Determines the Status of an DoctorEvent record.
    /// </summary>

    public enum AppointmentStatus
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,
        /// <summary>
        /// Indicates DoctorEvent Planned.
        /// </summary>
        [Display(Name = "Planned")]
        orange = 1,

        /// <summary>
        /// Indicates DoctorEvent Confirmed.
        /// </summary>
        [Display(Name = "Confirmed")]
        green = 2,

        /// <summary>
        /// Indicates DoctorEvent changed.
        /// </summary>
        [Display(Name = "Changed")]
        red = 3,

        /// <summary>
        /// Indicates DoctorEvent Completed.
        /// </summary>
        [Display(Name = "Completed")]
        darkcyan = 4       


    }
}
