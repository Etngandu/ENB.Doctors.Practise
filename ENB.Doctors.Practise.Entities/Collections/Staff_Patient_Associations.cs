using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Doctors.Practise.Entities.Collections
{
    
    /// <summary>
    /// Represents a collection of People instances in the system.
    /// </summary>
    public class Staff_Patient_Associations : CollectionBase<Staff_Patient_Association>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Operators"/> class.
        /// </summary>
        public Staff_Patient_Associations() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Operators"/> class.
        /// </summary>
        /// <param name="initialList">Accepts an IList of Person as the initial list.</param>
        public Staff_Patient_Associations(IList<Staff_Patient_Association> initialList) : base(initialList) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Operators"/> class.
        /// </summary>
        /// <param name="initialList">Accepts a CollectionBase of Person as the initial list.</param>
        public Staff_Patient_Associations(CollectionBase<Staff_Patient_Association> initialList) : base(initialList) { }

        /// <summary>
        /// Validates the current collection by validating each individual item in the collection.
        /// </summary>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var staff_patient_association  in this)
            {
                errors.AddRange(staff_patient_association.Validate());
            }
            return errors;
        }
    }
}
