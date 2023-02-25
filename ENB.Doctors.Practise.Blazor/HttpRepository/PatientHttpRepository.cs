using ENB.Doctors.Practise.Blazor.HttpRepository;
using ENB.Doctors.Practise.Blazor.Models;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ENB.Blazor.Lawyer.HttpRepository
{
    public class PatientHttpRepository : IPatientHttpRepository
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
      
        public PatientHttpRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<CreateAndEditPatient> AddPatient(CreateAndEditPatient createAndEditPatient)
        {
            
            var response = await _client.PostAsJsonAsync("patient/createpatient", createAndEditPatient);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var patient =  JsonSerializer.Deserialize<CreateAndEditPatient>(content, _options);
            return patient!;          //  return await response.Content.ReadFromJsonAsync<CreateAndEditPatient>();           

            
        }

    
       
        public  async Task DeletePatient(int Id)
        {
            await _client.DeleteAsync($"patient/deletepatient/{Id}");

        }

        public async Task<CreateAndEditPatient> EditPatient(CreateAndEditPatient createAndEditPatient)
        {
            var response = await _client.PutAsJsonAsync("patient/editpatient", createAndEditPatient);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var patient = JsonSerializer.Deserialize<CreateAndEditPatient>(content, _options);
            return patient!;
        }

        public async Task<DisplayPatient> GetPatient(int Id)
        {
            var response = await _client.GetAsync($"Patient/details/{Id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var patient = JsonSerializer.Deserialize<DisplayPatient>(content, _options);
            return patient!;
        }

        public async Task<List<DisplayPatient>> GetPatients()
        {
            var response = await _client.GetAsync("patient/allpatients");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var patients =  JsonSerializer.Deserialize<List<DisplayPatient>>(content, _options);
            return patients!;
        }

      public async  Task <CreateAndEditPatient> RetrievePatient(int Id)
        {
            var response = await _client.GetAsync($"Patient/retrievepatient/{Id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var patient = JsonSerializer.Deserialize<CreateAndEditPatient>(content, _options);
            return patient!;
        }
    }
}
