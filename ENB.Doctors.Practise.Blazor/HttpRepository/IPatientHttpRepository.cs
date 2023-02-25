using ENB.Doctors.Practise.Blazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENB.Doctors.Practise.Blazor.HttpRepository
{
  public  interface IPatientHttpRepository
    {
        Task <List<DisplayPatient>> GetPatients();        
        Task<DisplayPatient> GetPatient(int Id); 
        Task<CreateAndEditPatient> RetrievePatient(int Id);
        Task<CreateAndEditPatient> AddPatient(CreateAndEditPatient createAndEditPatient);
        Task<CreateAndEditPatient> EditPatient(CreateAndEditPatient createAndEditPatient);
        Task DeletePatient(int Id);
    }
}
