using ENB.Doctors.Practise.Blazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENB.Doctors.Practise.Blazor.HttpRepository
{
  public  interface IStaffHttpRepository
    {
        Task<List<DisplayStaff>> GetStaffs();
        Task<DisplayStaff> GetStaff(int Id);       
        Task<CreateAndEditStaff> RetrieveStaff(int Id);
        Task<CreateAndEditStaff> AddStaff(CreateAndEditStaff createAndEditStaff);
        Task<CreateAndEditStaff> EditStaff(CreateAndEditStaff createAndEditStaff);
        Task DeleteStaff(int Id);
    }
}
