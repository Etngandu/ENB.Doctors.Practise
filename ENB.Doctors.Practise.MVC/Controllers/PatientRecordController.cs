using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Doctors.Practise.Entities.Repositories;
using ENB.Doctors.Practise.Entities;
using ENB.Doctors.Practise.Infrastructure;
using ENB.Doctors.Practise.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ENB.Doctors.Practise.EF.Migrations;

namespace ENB.Doctors.Practise.MVC.Controllers
{
    public class PatientRecordController : Controller
    {
        // GET: Lawyer

        private readonly IAsyncPatientRepository _asyncPatientRepository;
        private readonly IAsyncStaffRepository _asyncStaffRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly IMapper _imapper;
        private readonly ILogger<StaffPatientAssociationController> _logger;
        private readonly INotyfService _notifyService;

        /// <summary>
        /// Initializes a new instance of the LawyerOnCaseController class.
        /// </summary>
        /// 

        public PatientRecordController(IAsyncPatientRepository asyncPatientRepository,
                                                  IAsyncStaffRepository asyncStaffRepository,
                                                  ILogger<StaffPatientAssociationController> logger,
                                                  IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                                  IMapper imapper, INotyfService notifyService)
        {
            _asyncPatientRepository = asyncPatientRepository;
            _asyncStaffRepository = asyncStaffRepository;
            _logger = logger;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _imapper = imapper;
            _notifyService = notifyService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RecordperPatientList(int PatientId)
        {
            ViewBag.Idpatient = PatientId;
            var patient = await _asyncPatientRepository.FindById(PatientId);

            ViewBag.Message = patient.FullName;
            return View();
        }

        public async Task<IActionResult> GetRecordperPatientList(int PatientId)

        {
            ViewBag.Idpatient = PatientId;

            var staffs = _asyncPatientRepository.FindAll().Where(l => l.Id == PatientId).SelectMany(l => l.Patient_Records)
                                                      .Join(_asyncStaffRepository.FindAll(),
                                                      ptrc => ptrc.StaffId,
                                                      st => st.Id,
                                                      (patientrec, staf) => new DisplayPatientRecord
                                                      {
                                                          Id = patientrec.Id,
                                                          NameStaff = staf.FullName, 
                                                          Component_Code=patientrec.Component_Code,
                                                          DateCreated = patientrec.DateCreated,
                                                          DateModified = patientrec.DateModified,
                                                          Other_Details = patientrec.Other_Details,
                                                      });




            var Mpdata = await Task.FromResult(_imapper.Map<List<DisplayPatientRecord>>(staffs));


            return Json(new { data = Mpdata });

        }

        public async Task<IActionResult> RecordperStaffList(int StaffId)
        {
            ViewBag.Idstaff = StaffId;
            var staff = await _asyncStaffRepository.FindById(StaffId);

            ViewBag.Message = staff.FullName;
            return View();
        }
        public async Task<IActionResult> GetRecordperStaffList(int StaffId)

        {
            ViewBag.Idstaff = StaffId;

            var patients = _asyncStaffRepository.FindAll().Where(l => l.Id == StaffId).SelectMany(l => l.Patient_Records)
                                                      .Join(_asyncPatientRepository.FindAll(),
                                                      ptrc => ptrc.PatientId,
                                                      pt => pt.Id,
                                                      (patientrec, pat) => new DisplayPatientRecord
                                                      {
                                                          Id = patientrec.Id,
                                                          NamePatient = pat.FullName,
                                                          Component_Code=patientrec.Component_Code,
                                                          DateCreated = patientrec.DateCreated,
                                                          DateModified = patientrec.DateModified,
                                                          Other_Details = patientrec.Other_Details,

                                                      });


            var pat = await _asyncStaffRepository.FindById(StaffId);

            ViewBag.Message = pat.FullName;

            var Mpdata = await Task.FromResult(_imapper.Map<List<DisplayPatientRecord>>(patients));

            return Json(new { data = Mpdata });
        }

        [HttpGet]
        public async Task<IActionResult> RecordperPatientCreate(int PatientId)
        {
            ViewBag.Idpatient = PatientId;

            var data = new CreateAndEditPatientRecord()
            {
                ListStaff = _asyncStaffRepository.FindAll()
                        .Select(d => new SelectListItem
                        {
                            Text = d.FullName,
                            Value = d.Id.ToString(),
                            Selected = true

                        }).Distinct().ToList()



            };

            var Nlawyer = await _asyncPatientRepository.FindById(PatientId);

            ViewBag.Message = Nlawyer.FullName;

            return View(data);
        }


        public async Task<IActionResult> RecordperStaffCreate(int StaffId)
        {
            ViewBag.Idstaff = StaffId;
            var data = new CreateAndEditPatientRecord()
            {
                ListPatient = _asyncPatientRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.FullName,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()


            };

            var patient = await _asyncPatientRepository.FindById(StaffId);

            ViewBag.Message = patient.FullName;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecordperPatientCreate(CreateAndEditPatientRecord createAndEditPatientRecord,
                                                                  int PatientId)
        {
            ViewBag.PatientId = PatientId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var patient = await _asyncPatientRepository.FindById(PatientId);


                        var staffpatient = new Patient_Record();

                        

                        ViewBag.Message = patient.FullName;


                        _imapper.Map(createAndEditPatientRecord, staffpatient);

                        patient.Patient_Records.Add(staffpatient);

                        _notifyService.Success($"Staff related to Patient {ViewBag.Message} Successfully ");

                        return RedirectToAction(nameof(RecordperPatientList), new { PatientId });
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecordperStaffCreate(CreateAndEditPatientRecord createAndEditPatientRecord,
                                                     int StaffId)
        {
            ViewBag.Idstaff = StaffId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var staff = await _asyncStaffRepository.FindById(StaffId);

                        var staffpatient = new Patient_Record();

                        ViewBag.Message = staff.FullName;


                        _imapper.Map(createAndEditPatientRecord, staffpatient);

                        staff.Patient_Records.Add(staffpatient);

                        _notifyService.Success($"Patient related to Staff {ViewBag.Message}  Successfully");

                        return RedirectToAction("RecordperStaffList", new { StaffId });
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


        public async Task<IActionResult> RecordperPatientEdit(int PatientId, int Id)
        {
            ViewBag.Idpatient = PatientId;
            ViewBag.Id = Id;

            var patient = await _asyncPatientRepository.FindById(PatientId);
            ViewBag.Message = patient.FullName;

            var staffpatient = await _asyncPatientRepository.FindById(PatientId, x => x.Patient_Records);
            if (staffpatient is null)
            {
                return NotFound();
            }

            var data = new CreateAndEditPatientRecord()
            {

                ListStaff = _asyncStaffRepository.FindAll()
                        .Select(d => new SelectListItem
                        {
                            Text = d.FullName,
                            Value = d.Id.ToString(),
                            Selected = true

                        }).Distinct().ToList()


            };

            _imapper.Map(staffpatient.Patient_Records.Single(x => x.Id == Id), data);

            return View(data);
        }

        public async Task<ActionResult> RecordperStaffEdit(int StaffId, int id)
        {
            ViewBag.Idstaff = StaffId;
            ViewBag.Id = id;

            var staff = await _asyncStaffRepository.FindById(StaffId);
            ViewBag.Message = staff.FullName;

            var staffpatient = await _asyncStaffRepository.FindById(StaffId, x => x.Patient_Records);

            if (staffpatient == null)
            {
                return NotFound();
            }


            var data = new CreateAndEditPatientRecord()
            {
                ListPatient = _asyncPatientRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.FullName,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()




            };
            _imapper.Map(staffpatient.Patient_Records.Single(x => x.Id == id), data);




            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecordperPatientEdit(CreateAndEditPatientRecord createAndEditPatientRecord,
                                                  int PatientId)
        {

            ViewBag.Idpatient = PatientId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var patientstaffs = await _asyncPatientRepository.FindById(PatientId, x => x.Patient_Records);
                        var patientstaff = patientstaffs.Patient_Records.Single(x => x.Id == createAndEditPatientRecord.Id);

                        _imapper.Map(createAndEditPatientRecord, patientstaff);

                        _notifyService.Success("Staff related to Patient  updated Successfully");

                        return RedirectToAction(nameof(RecordperPatientList), new { PatientId });
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecordperStaffEdit(CreateAndEditPatientRecord createAndEditPatientRecord,
                                                  int StaffId)
        {

            ViewBag.Idstaff = StaffId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var staffpatients = await _asyncStaffRepository.FindById(StaffId, x => x.Patient_Records);
                        var staffpatient = staffpatients.Patient_Records.Single(x => x.Id == createAndEditPatientRecord.Id);

                        _imapper.Map(createAndEditPatientRecord, staffpatient);

                        _notifyService.Success("patient record related to staff update Successfully");

                        return RedirectToAction(nameof(RecordperStaffList), new { StaffId });
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

        public async Task<ActionResult> RecordperPatientDetails(int id, int PatientId)
        {

            ViewBag.Idpatient = PatientId;
            ViewBag.Id = id;

            var staffpatients = await _asyncPatientRepository.FindById(PatientId, x => x.Patient_Records);
            var staffpatientsfil = staffpatients.Patient_Records
                                                     .Join(_asyncStaffRepository.FindAll(),
                                                     ptrec => ptrec.StaffId,
                                                     st => st.Id,
                                                     (patientrec, stf) => new DisplayPatientRecord
                                                     {
                                                         Id = patientrec.Id,
                                                         NameStaff = stf.FullName,
                                                         Component_Code=patientrec.Component_Code,
                                                         Other_Details = patientrec.Other_Details,
                                                         DateCreated = patientrec.DateCreated,
                                                         DateModified = patientrec.DateModified
                                                     }).ToList();


            var patient = await _asyncPatientRepository.FindById(PatientId);

            ViewBag.Message = patient.FullName;

            var sgl = staffpatientsfil.Single(x => x.Id == id);


            return View(sgl);
        }


        public async Task<ActionResult> RecordperStaffDetails(int id, int StaffId)
        {

            ViewBag.Idstaff = StaffId;
            ViewBag.Id = id;

            var patientstaffs = await _asyncStaffRepository.FindById(StaffId, l => l.Patient_Records);
            var patientstaffsfil = patientstaffs.Patient_Records
                                                      .Join(_asyncPatientRepository.FindAll(),
                                                      ptrec => ptrec.PatientId,
                                                      pt => pt.Id,
                                                      (patientrec, pat) => new DisplayPatientRecord
                                                      {
                                                          Id = patientrec.Id,
                                                          NamePatient = pat.FullName,  
                                                          Component_Code=patientrec.Component_Code,
                                                          DateCreated = patientrec.DateCreated,
                                                          DateModified = patientrec.DateModified,
                                                          Other_Details = patientrec.Other_Details
                                                      }).ToList();


            var staff = await _asyncStaffRepository.FindById(StaffId);

            ViewBag.Message = staff.FullName;

            var sgl = patientstaffsfil.Single(x => x.Id == id);


            return View(sgl);
        }

        public async Task<ActionResult> RecordperPatientDelete(int id, int PatientId)
        {

            ViewBag.Idpatient = PatientId;
            ViewBag.Id = id;

            var staffpatients = await _asyncPatientRepository.FindById(PatientId, x => x.Patient_Records);
            var staffpatientsfil = staffpatients.Patient_Records
                                                     .Join(_asyncStaffRepository.FindAll(),
                                                     ptrec => ptrec.StaffId,
                                                     st => st.Id,
                                                     (patientrec, stf) => new DisplayPatientRecord
                                                     {
                                                         Id = patientrec.Id,
                                                         NameStaff = stf.FullName,
                                                         Component_Code=patientrec.Component_Code,
                                                         Other_Details = patientrec.Other_Details,
                                                         DateCreated = patientrec.DateCreated,
                                                         DateModified = patientrec.DateModified
                                                     }).ToList();


            var patient = await _asyncPatientRepository.FindById(PatientId);

            ViewBag.Message = patient.FullName;

            var sgl = staffpatientsfil.Single(x => x.Id == id);


            return View(sgl);
        }

        public async Task<ActionResult> RecordperStaffDelete(int id, int StaffId)
        {

            ViewBag.Idstaff = StaffId;
            ViewBag.Id = id;

            var patientstaffs = await _asyncStaffRepository.FindById(StaffId, l => l.Patient_Records);
            var patientstaffsfil = patientstaffs.Patient_Records
                                                      .Join(_asyncPatientRepository.FindAll(),
                                                      ptrec => ptrec.PatientId,
                                                      pt => pt.Id,
                                                      (patientrec, pat) => new DisplayPatientRecord
                                                      {
                                                          Id = patientrec.Id,
                                                          NamePatient = pat.FullName,  
                                                          Component_Code=patientrec.Component_Code,
                                                          DateCreated = patientrec.DateCreated,
                                                          DateModified = patientrec.DateModified,
                                                          Other_Details = patientrec.Other_Details
                                                      }).ToList();


            var staff = await _asyncStaffRepository.FindById(StaffId);

            ViewBag.Message = staff.FullName;

            var sgl = patientstaffsfil.Single(x => x.Id == id);


            return View(sgl);
        }
        [HttpPost, ActionName("DeleteStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecordperPatientConfirmed(DisplayPatientRecord displayPatientRecord,
                                                    int PatientId)
        {
            ViewBag.Idpatient = PatientId;

            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var patientstaffs = await _asyncPatientRepository.FindById(PatientId, x => x.Patient_Records);
                var patientstaff = patientstaffs.Patient_Records.Single(x => x.Id == displayPatientRecord.Id);

                patientstaffs.Patient_Records.Remove(patientstaff);

                _notifyService.Error("Staff related to Patient removed  Successfully");
            }
            return RedirectToAction(nameof(RecordperPatientList), new { PatientId });
        }

        // POST: LawyersOnCasesController/Delete/5
        [HttpPost, ActionName("DeletePatient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecordperStaffConfirmed(DisplayPatientRecord displayPatientRecord,
                                                            int StaffId)
        {
            ViewBag.Idstaff = StaffId;
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var staffpatients = await _asyncStaffRepository.FindById(StaffId, x => x.Patient_Records);
                var staffpatient = staffpatients.Patient_Records.Single(x => x.Id == displayPatientRecord.Id);

                staffpatients.Patient_Records.Remove(staffpatient);
                _notifyService.Error("Patient related to Staff removed  Successfully");
            }
            return RedirectToAction(nameof(RecordperStaffList), new { StaffId });
        }
    }
}
