using ENB.Doctors.Practise.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENB.Doctors.Practise.Entities
{
    public enum Staff_JobTitle
    {
        // <summary>
        /// Determines the Component_Code.
        /// </summary>

        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a  Component_Code : senior consultants.
        /// </summary>
        [Display(Name = "senior consultants")]
        senior_consultants = 1,

        /// <summary>
        /// Indicates a Component_Code : registrars.
        /// </summary>        
        [Display(Name = "registrars")]
        registrars = 2,

        /// <summary>
        /// Indicates a  Component_Code : residents.
        /// </summary>
        [Display(Name = "residents")]
        residents = 3,

        /// <summary>
        /// Indicates a  Component_Code : student doctors.
        /// </summary>
        [Display(Name = "student doctors")]
        student_doctors = 4,

        /// <summary>
        /// Indicates a Component_Code : nurse unit manager.
        /// </summary>        
        [Display(Name = "nurse unit manager")]
        nurse_unit_manager = 5,

        /// <summary>
        /// Indicates a  Component_Code : nurse practitioners.
        /// </summary>
        [Display(Name = "nurse practitioners")]
        nurse_practitioners = 6,

        /// <summary>
        /// Indicates a  Component_Code : specialist nurses.
        /// </summary>
        [Display(Name = "specialist nurses")]
        specialist_nurses = 7,

        /// <summary>
        /// Indicates a  Component_Code : registered nurses.
        /// </summary>
        [Display(Name = "registered nurses")]
        registered_nurses = 8,

        /// <summary>
        /// Indicates a  Component_Code : enrolled nurses.
        /// </summary>
        [Display(Name = "enrolled nurses")]
        enrolled_nurses = 9,

        /// <summary>
        /// Indicates a  Component_Code : dietitians.
        /// </summary>
        [Display(Name = "dietitians")]
        dietitians = 10,

        /// <summary>
        /// Indicates a  Component_Code : pharmacists.
        /// </summary>
        [Display(Name = "pharmacists")]
        pharmacists = 11,

        /// <summary>
        /// Indicates a Component_Code : podiatrists.
        /// </summary>        
        [Display(Name = "podiatrists")]
        podiatrists = 12,

        /// <summary>
        /// Indicates a Component_Code : physiotherapists.
        /// </summary>        
        [Display(Name = "physiotherapists")]
        physiotherapists = 13,

        /// <summary>
        /// Indicates a  Component_Code : speech_pathologists.
        /// </summary>
        [Display(Name = "speech_pathologists")]
        speech_pathologists = 14,

        /// <summary>
        /// Indicates a  Component_Code : clinical assistants.
        /// </summary>
        [Display(Name = "clinical assistants")]
        clinical_assistants = 15,

        /// <summary>
        /// Indicates a  Component_Code : patient services assistants.
        /// </summary>
        [Display(Name = "patient services assistants")]
        patient_services_assistants = 16,

        /// <summary>
        /// Indicates a  Component_Code : porters.
        /// </summary>
        [Display(Name = "porters ")]
        porters = 17,

        /// <summary>
        /// Indicates a  Component_Code : volunteers.
        /// </summary>
        [Display(Name = "volunteers")]
        volunteers = 18,

        /// <summary>
        /// Indicates a  Component_Code : ward clerks.
        /// </summary>
        [Display(Name = "ward clerks")]
        ward_clerks = 19
    }
}
