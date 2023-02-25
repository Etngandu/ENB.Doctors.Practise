using AutoMapper;
using ENB.Doctors.Practise.Entities;
using ENB.Doctors.Practise.Entities.Repositories;
using ENB.Doctors.Practise.Infrastructure;
using ENB.Doctors.Practise.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENB.Doctors.Practise.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<StaffController> _logger;
        private readonly IAsyncStaffRepository _asyncStaffRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;

        public StaffController(IMapper mapper, ILogger<StaffController> logger,
                                   IAsyncStaffRepository asyncStaffRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory
                                   )
        {
            _mapper = mapper;
            _logger = logger;
            _asyncStaffRepository = asyncStaffRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;

        }

        // GET: CustomerController

        [HttpGet]
        [Route("AllStaffMembers")]
        public async Task<IActionResult> GetStaffData()
        {
            IQueryable<Staff> allStaff = _asyncStaffRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayStaff>>(allStaff).ToList();
            await Task.FromResult(Mpdata);
            return Ok (Mpdata);
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            

            _logger.LogError($"Id :{id} of Customer not found");

            Staff dbStaff = await _asyncStaffRepository.FindById(id);

          

           // _logger.LogInformation($"Details of Staff: {ViewBag.Message}");

            if (dbStaff is null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplayStaff>(dbStaff);

            return Ok(data);
        }



       
        [HttpPost]
        [Route("createStaff")]
        public async Task<IActionResult> Create(CreateAndEditStaff createAndEditStaff)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        Staff dbStaff = new();

                        _mapper.Map(createAndEditStaff, dbStaff);
                        await _asyncStaffRepository.Add(dbStaff);

                       // _notyf.Success("Staff Created  Successfully! ");

                        return Ok(createAndEditStaff);
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
                    }
                }
            }
            return Ok(createAndEditStaff);
        }

        [HttpGet]
        [Route("retrieveStaff/{id}")]
        public async Task<IActionResult> RetrieveStaff(int id)
        {



            Staff dbStaff = await _asyncStaffRepository.FindById(id);

            if (dbStaff is null)
            {
                _logger.LogError($"Staff {id} not found");
                return NotFound();
            }
            var data = await Task.FromResult(_mapper.Map<CreateAndEditStaff>(dbStaff));

            return Ok(data);
        }

        
        [HttpPut]
        [Route("editStaff")]
        public async Task<IActionResult> Edit(CreateAndEditStaff createAndEditStaff)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        Staff dbStaffToUpdate = await _asyncStaffRepository.FindById(createAndEditStaff.Id);

                        _mapper.Map(createAndEditStaff, dbStaffToUpdate, typeof(CreateAndEditStaff), typeof(Staff));



                        return Ok(createAndEditStaff);
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
                    }
                }
            }
            return Ok(createAndEditStaff);
        }



        [HttpDelete]
        [Route("deletestaff/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Staff dbStaff = await _asyncStaffRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                _asyncStaffRepository.Remove(dbStaff);
                
            }

            return Ok(); ;
        }
    }
}
