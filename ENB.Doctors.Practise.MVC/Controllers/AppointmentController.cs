using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Doctors.Practise.Entities.Repositories;
using ENB.Doctors.Practise.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ENB.Doctors.Practise.MVC.Models;
using ENB.Doctors.Practise.Entities;

namespace ENB.Doctors.Practise.MVC.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAsyncPatientRepository _asyncPatientRepository;
        private readonly IAsyncStaffRepository _asyncStaffRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        public AppointmentController(IMapper mapper, ILogger<AppointmentController> logger,
                                   IAsyncPatientRepository asyncPatientRepository,
                                   IAsyncStaffRepository asyncStaffRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf)
        {
            _mapper = mapper;
            _logger = logger;
            _asyncPatientRepository = asyncPatientRepository;
            _asyncStaffRepository = asyncStaffRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;
        }
        public IActionResult Index(DateTime? eventDate)
        {
            ViewBag.EventDate = eventDate ?? DateTime.Now;
            return View();
        }

        public async Task<IActionResult> List(int PatientId)
        {
            ViewBag.Idpatient = PatientId;
            var patient = await _asyncPatientRepository.FindById(PatientId);

            ViewBag.Message = patient.FullName;

            return View();
        }


        public JsonResult GetListEvents(int PatientId)
        {

            var lwyrevents = (from bkap in _asyncPatientRepository.FindAll().Where(x => x.Id == PatientId).SelectMany(pat => pat.Appointments)
                              join pat in _asyncPatientRepository.FindAll() on bkap.PatientId equals pat.Id
                              select new DisplayAppointment
                              {
                                  Id = bkap.Id,
                                  PatientId = bkap.PatientId,
                                  BookingNumber = bkap.BookingNumber,
                                  Start = bkap.Start,
                                  EventStatus = bkap.EventStatus,                                 
                                  End = bkap.End,
                                  Color = bkap.Color,
                                  AllDay = bkap.AllDay
                              }).ToArray();

            return Json(new { data = lwyrevents });


        }


        [HttpGet]
        public IActionResult CreateCal(string eventDate)
        {
            ViewBag.EventDate = eventDate;
            var data = new CreateAndEditAppointment()
            {
                ListPatient = _asyncPatientRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.FullName,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList(),

                ListStaff = _asyncStaffRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.FullName,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()


            };



            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> CreateEvent(int PatientId)
        {
            ViewBag.Idpatient = PatientId;

            var patient = await _asyncPatientRepository.FindById(PatientId);
            var data = new CreateAndEditAppointment()
            {
                ListStaff = _asyncStaffRepository.FindAll()
                      .Select(d => new SelectListItem
                      {
                          Text = d.FullName,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList(),
                


            };

            ViewBag.Message = patient.FullName;

            return View(data);
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditAppointment CreateAndEditAppointment)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var patient = await _asyncPatientRepository.FindById(CreateAndEditAppointment.PatientId);
                        Appointment patientbooking = new();

                        _mapper.Map(CreateAndEditAppointment, patientbooking);

                        patient.Appointments.Add(patientbooking);

                        _notyf.Success("Booking event Added  Successfully! ");

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

        public JsonResult GetEvents()
        {

            var lwyrevents = (from bkap in _asyncPatientRepository.FindAll().SelectMany(pat => pat.Appointments)
                                  //join cst in _asyncPatientRepository.FindAll() on bkap.PatientId equals cst.Id
                              select new DisplayAppointment
                              {
                                  Id = bkap.Id,
                                  PatientId = bkap.PatientId,
                                  BookingNumber = bkap.BookingNumber,
                                  Title = "Booking  " + bkap.Patient!.FullName,
                                  Description = bkap.Other_details,
                                  Start = bkap.Start,
                                  End = bkap.End,
                                  Color = bkap.Color,
                                  AllDay = bkap.AllDay
                              }).ToArray();

            return Json(lwyrevents);


        }

        public async Task<IActionResult> EditEvent(int PatientId, int id)
        {

            var patient = await _asyncPatientRepository.FindById(PatientId);
            ViewBag.Message = patient.FullName;
            ViewBag.Idpatient = PatientId;
            ViewBag.Id = id;

            var patientevt = await _asyncPatientRepository.FindById(PatientId, ev => ev.Appointments);

            if (patientevt == null)
            {
                return NotFound();
            }

            var data = new CreateAndEditAppointment()
            {
                ListStaff = _asyncStaffRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.FullName,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()


            };

            _mapper.Map(patientevt.Appointments.Single(x => x.Id == id), data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditAppointment CreateAndEditAppointment, int PatientId)
        {

            ViewBag.PatientId = PatientId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var patientevet = await _asyncPatientRepository.FindById(PatientId, x => x.Appointments);
                        var bookingevent = patientevet.Appointments.Single(x => x.Id == CreateAndEditAppointment.Id);

                        _mapper.Map(CreateAndEditAppointment, bookingevent);

                        _notyf.Success($"Event related to patient{patientevet.FullName} updated Successfully");

                        return RedirectToAction(nameof(List), new { PatientId });
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

        public async Task<IActionResult> Details(int PatientId, int id)
        {

            var patient = await _asyncPatientRepository.FindById(PatientId);
            ViewBag.Message = patient.FullName;
            ViewBag.Idpatient = PatientId;
            ViewBag.Id = id;

            var patientevent = await _asyncPatientRepository.FindById(PatientId, ev => ev.Appointments);

            if (patientevent is null)
            {
                return NotFound();
            }
            var myevent = new DisplayAppointment();

            var sglevent = _mapper.Map(patientevent.Appointments.Single(x => x.Id == id), myevent);

            // sglevent.Color = Enum.GetName(typeof(EventStatus), Int32.Parse(sglevent.Color!));
            return View(sglevent);
        }

        public async Task<IActionResult> Delete(int PatientId, int id)
        {

            var patient = await _asyncPatientRepository.FindById(PatientId);
            ViewBag.Message = patient.FullName;
            ViewBag.Idpatient = PatientId;
            ViewBag.Id = id;

            var patientevent = await _asyncPatientRepository.FindById(PatientId, ev => ev.Appointments);

            if (patientevent is null)
            {
                return NotFound();
            }
            var myevent = new DisplayAppointment();

            var sglevent = _mapper.Map(patientevent.Appointments.Single(x => x.Id == id), myevent);

            // sglevent.Color = Enum.GetName(typeof(EventStatus), Int32.Parse(sglevent.Color!));
            return View(sglevent);
        }

        // POST: BookingEventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayAppointment DisplayAppointment, int PatientId)
        {

            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var patient = await _asyncPatientRepository.FindById(PatientId, x => x.Appointments);
                var patientevent = patient.Appointments.Single(x => x.Id == DisplayAppointment.Id);

                patient.Appointments.Remove(patientevent);

                _notyf.Error($"Event related to patient {patient.FullName} removed  Successfully");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
