using ENB.Doctors.Practise.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ENB.Doctors.Practise.EF
{
  public  class AsyncEFUnitOfWorkFactory :IAsyncUnitOfWorkFactory
    {
        private readonly DoctorPractiseContext _doctorPractiseContext;

      

        public AsyncEFUnitOfWorkFactory(DoctorPractiseContext doctorPractiseContext)
        {
            _doctorPractiseContext = doctorPractiseContext;

        }
        public AsyncEFUnitOfWorkFactory(bool forcenew, DoctorPractiseContext doctorPractiseContext)
        {
            _doctorPractiseContext = doctorPractiseContext;

        }
        /// <summary>
        /// Creates a new instance of an EFUnitOfWork.
        /// </summary>
        public async Task<IAsyncUnitOfWork> Create()
        {
            return await Create(false);
        }

        /// <summary>
        /// Creates a new instance of an EFUnitOfWork.
        /// </summary>
        /// <param name="forceNew">When true, clears out any existing data context from the storage container.</param>
        public async Task<IAsyncUnitOfWork> Create(bool forceNew)
        {
            var asyncEFUnitOfWork = await Task.FromResult(new AsyncEFUnitOfWork(forceNew, _doctorPractiseContext));


            return asyncEFUnitOfWork!;

        }


    }
}
