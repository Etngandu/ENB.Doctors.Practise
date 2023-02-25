
using ENB.Doctors.Practise.Blazor.HttpRepository;
using ENB.Doctors.Practise.Blazor.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ENB.Blazor.Lawyer.HttpRepository
{
    public class StaffHttpRepository : IStaffHttpRepository
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
      
        public StaffHttpRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<CreateAndEditStaff> AddStaff(CreateAndEditStaff createAndEditStaff)
        {
            var response = await _client.PostAsJsonAsync("staff/createstaff", createAndEditStaff);
            var content= await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var addedstaff = JsonSerializer.Deserialize<CreateAndEditStaff>(content, _options);
            return addedstaff!;           
        }


        public async Task DeleteStaff(int Id)
        {
            await _client.DeleteAsync($"staff/deletestaff/{Id}");

        }
        public async   Task<CreateAndEditStaff> RetrieveStaff(int Id)
        {
            var response = await _client.GetAsync($"staff/displaystaff/{Id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var staff = JsonSerializer.Deserialize<CreateAndEditStaff>(content, _options);
            return staff!;
        }

      public async  Task<CreateAndEditStaff> EditStaff(CreateAndEditStaff createAndEditStaff)
        {
            var response = await _client.PutAsJsonAsync("staff/editstaff", createAndEditStaff);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var modifystaff = JsonSerializer.Deserialize<CreateAndEditStaff>(content, _options);
            return modifystaff!;
           
        }

      public async  Task<DisplayStaff> GetStaff(int Id)
        {
            var response = await _client.GetAsync($"staff/detailsstaff/{Id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var staff = JsonSerializer.Deserialize<DisplayStaff>(content, _options);
            return staff!;
        }

      public async  Task<List<DisplayStaff>> GetStaffs()
        {
            var response = await _client.GetAsync("Staff/AllStaffMembers");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var staffs = JsonSerializer.Deserialize<List<DisplayStaff>>(content, _options);
            return staffs!;
        }

       
    }
}
