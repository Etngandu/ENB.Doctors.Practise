using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Doctors.Practise.Entities
{
    public enum Staff_Category_Code
    {
        // <summary>
        /// Determines the Component_Code.
        /// </summary>

        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a  Component_Code : Doctors (medical staff).
        /// </summary>
        [Display(Name = "Doctors (medical staff)")]
        Doctors_medical_staff = 1,

        /// <summary>
        /// Indicates a Component_Code : Nurses.
        /// </summary>        
        [Display(Name = "Nurses")]
        Nurses = 2,

        /// <summary>
        /// Indicates a  Component_Code : Allied_health_professionals.
        /// </summary>
        [Display(Name = "Allied health professionals")]
        Allied_health_professionals = 3,


        /// <summary>
        /// Indicates a  Component_Code : Admission.
        /// </summary>
        [Display(Name = "Other hospital staff")]
        Other_hospital_staff = 4
        
    }
}
