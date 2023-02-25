using System.ComponentModel.DataAnnotations;

namespace ENB.Doctors.Practise.Entities
{
    /// <summary>
    /// Determines the type of a contact record.
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a Male Guest.
        /// </summary>        
        Male = 1,

        /// <summary>
        /// Indicates a Female Guest.
        /// </summary>        
        Female = 2        


    }
}
