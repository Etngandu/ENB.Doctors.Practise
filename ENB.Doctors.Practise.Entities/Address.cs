using ENB.Doctors.Practise.Entities.Collections;
using ENB.Doctors.Practise.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Doctors.Practise.Entities
{
    public class Address : ValueObject<Address>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Address class.
        /// </summary>
        public Address(string number_street, string city, string zipcode,
                   string state_province_county, string country)
        {
            
            Number_street=number_street;
            City=city;  
            Zipcode=zipcode;
            State_province_county=state_province_county;
            Country=country;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Address()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        #endregion

        #region "Public Properties"


        /// </summary>
        /// 
        public string Number_street { get; private set; }

        /// <summary>
        /// Gets or sets the City of the Customer.
        /// </summary>
        /// 
        public string City { get; private set; }
        /// <summary>
        /// Gets or sets the Zipcode of the Customer.
        /// </summary>
        /// 
        public string Zipcode { get;private set; }   

        /// <summary>
        /// Gets or sets the State_province_county of the Customer.
        /// </summary>
        /// 
        public string State_province_county { get; private set; } 

        /// <summary>
        /// Gets or sets the Country of the Customer.
        /// </summary>
        /// 
        public string Country { get; private set; } 

        /// <summary>
        /// Determines if this address can be considered to represent a "null" value.
        /// </summary>
        // <returns>True when all four properties of the address contain null; false otherwise. 
        public bool IsNull
        {
            get
            {
                return (string.IsNullOrEmpty(Number_street) && string.IsNullOrEmpty(City) && string.IsNullOrEmpty(State_province_county)
                    && string.IsNullOrEmpty(Zipcode) && string.IsNullOrEmpty(Country));
            }
        }
        #endregion


        #region Methods

        /// <summary>
        /// Validates this object. 
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsNull)
            {

                if (string.IsNullOrEmpty(Number_street))
                {
                    yield return new ValidationResult("Number_street can't be null or empty", new[] { "Number_street" });
                }
                if (string.IsNullOrEmpty(City))
                {
                    yield return new ValidationResult("City can't be null or empty", new[] { "City" });
                }
                if (string.IsNullOrEmpty(Zipcode))
                {
                    yield return new ValidationResult("Zipcode can't be null or empty", new[] { "Zipcode" });
                }
                if (string.IsNullOrEmpty(State_province_county))
                {
                    yield return new ValidationResult("State_province_county can't be null or empty", new[] { "State_province_county" });
                }
                if (string.IsNullOrEmpty(Country))
                {
                    yield return new ValidationResult("Country can't be null or empty", new[] { "Country" });
                }
            }
        }

        //protected override IEnumerable<object> GetEqualityComponents()
        //{
        //    // Using a yield return statement to return each element one at a time
        //    yield return Number_street;
        //    yield return City;
        //    yield return State_province_county;
        //    yield return Country;
        //    yield return Zipcode;
        //}
        #endregion
    }
}
