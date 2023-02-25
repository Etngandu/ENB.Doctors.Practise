using ENB.Doctors.Practise.Entities;
using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Doctors.Practise.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Doctors.Practise.EF.Repositories
{

    /// <summary>
    /// A concrete repository to work with case in the system.
    /// </summary>
    public class AsyncPatientRepository: AsyncRepository<Patient>, IAsyncPatientRepository
    {
        /// <summary>
        /// Gets a list of all guests whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Person with the matching people.</returns>
        /// 

        private readonly DoctorPractiseContext _doctorPractiseContext;
        public AsyncPatientRepository(DoctorPractiseContext doctorPractiseContext) : base(doctorPractiseContext)
        {
            _doctorPractiseContext = doctorPractiseContext;
        }
        public IEnumerable<Patient> FindByName(string lastname)
        {
            return _doctorPractiseContext.Set<Patient>().Where(x => x.Patient_last_name == lastname);
        }
    }
}
