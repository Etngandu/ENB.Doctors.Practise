using System.ComponentModel.DataAnnotations;

namespace ENB.PharmaciesAndPrescriptions.Entities
{
    /// <summary>
    /// Determines the type of a contact record.
    /// </summary>
    public enum Ref_payment_method
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a business contact record.
        /// </summary>
        Cash = 1,

        [Display(Name = "Credit card")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Credit_card = 2
    }
}
