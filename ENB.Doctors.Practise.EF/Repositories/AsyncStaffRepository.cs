using ENB.Doctors.Practise.EF;
using ENB.Doctors.Practise.EF.Repositories;
using ENB.Doctors.Practise.Entities;
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
    public class AsyncStaffRepository: AsyncRepository<Staff>, IAsyncStaffRepository
    {
        /// <summary>
        /// Gets a list of all guests whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Person with the matching people.</returns>
        /// 

        private readonly DoctorPractiseContext _doctorPractiseContext;
        public AsyncStaffRepository(DoctorPractiseContext doctorPractiseContext) : base(doctorPractiseContext)
        {
            _doctorPractiseContext = doctorPractiseContext;
        }
        public IEnumerable<Staff> FindByName(string lastname)
        {
            return _doctorPractiseContext.Set<Staff>().Where(x => x.Staff_last_name == lastname);
        }

       
    }
}
