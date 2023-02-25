using System.ComponentModel.DataAnnotations;

namespace ENB.Doctors.Practise.Entities
{
    /// <summary>
    /// Determines the type of a Component record.
    /// </summary>
    public enum Component_Code
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a Admission Component record.
        /// </summary>
        Admission = 1,

       
        /// <summary>
        /// Indicates a Diagnosis Component record.
        /// </summary>
        Diagnosis = 2,


        /// <summary>
        /// Indicates a Medication Component record.
        /// </summary>
        Medication = 3,

        [Display(Name = "Vital Sign")]
        /// <summary>
        /// Indicates a Admission Component record.
        /// </summary>
        
        Vital_signs = 4,

        [Display(Name = "Progress notes")]
        /// <summary>
        /// Indicates a Diagnosis Component record.
        /// </summary>
        Progress_notes = 5,

        [Display(Name = "Medical history")]
        /// <summary>
        /// Indicates a Medication Component record.
        /// </summary>
        Medical_histories = 6
    }
}
