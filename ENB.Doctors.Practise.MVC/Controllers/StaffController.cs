using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Doctors.Practise.Entities;
using ENB.Doctors.Practise.Entities.Repositories;
using ENB.Doctors.Practise.Infrastructure;
using ENB.Doctors.Practise.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENB.Doctors.Practise.MVC.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<StaffController> _logger;
        private readonly IAsyncStaffRepository _asyncStaffRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        public StaffController(IMapper mapper, ILogger<StaffController> logger,
                                   IAsyncStaffRepository asyncStaffRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf)
        {
            _mapper = mapper;
            _logger = logger;
            _asyncStaffRepository = asyncStaffRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetStaffData()
        {
            IQueryable<Staff> allStaff = _asyncStaffRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayStaff>>(allStaff).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // GET: CustomerController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Id = id;

            _logger.LogError($"Id :{id} of Customer not found");

            Staff dbStaff = await _asyncStaffRepository.FindById(id);

            ViewBag.Message = dbStaff.FullName;

            _logger.LogInformation($"Details of Staff: {ViewBag.Message}");

            if (dbStaff == null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplayStaff>(dbStaff);

            return View(data);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                        _notyf.Success("Staff Created  Successfully! ");

                        return RedirectToAction("Index");
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
            return View();
        }

        // GET: CustomerController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

           

            Staff dbStaff = await _asyncStaffRepository.FindById(id);

            if (dbStaff == null)
            {
               _logger.LogError($"Staff {id} not found");
                return NotFound();
            }
            var data = await Task.FromResult(_mapper.Map<CreateAndEditStaff>(dbStaff));

            return View(data);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                        _notyf.Success("Staff Updated  Successfully! ");

                        return RedirectToAction(nameof(Index));
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
            return View();
        }

        // GET: CustomerController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Staff dbStaff = await _asyncStaffRepository.FindById(id);
            ViewBag.Message = dbStaff.FullName;

            if (dbStaff == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<DisplayStaff>(dbStaff);
            return View(data);
        }

        // POST: CustomerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Staff dbStaff = await _asyncStaffRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                _asyncStaffRepository.Remove(dbStaff);

                _notyf.Error("Customer Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
