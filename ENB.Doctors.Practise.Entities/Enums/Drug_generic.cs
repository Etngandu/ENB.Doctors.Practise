using System.ComponentModel.DataAnnotations;

namespace ENB.Doctors.Practise.Entities
{
    /// <summary>
    /// Determines the type of a contact record.
    /// </summary>
    public enum Drug_generic
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates the drug is generic.
        /// </summary>       
        Yes = 1,

        /// <summary>
        /// Indicates the Drug is not generic.
        /// </summary>        
        No = 2

    }
}
