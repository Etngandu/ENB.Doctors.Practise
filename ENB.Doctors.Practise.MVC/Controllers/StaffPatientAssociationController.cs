using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Doctors.Practise.Infrastructure;
using ENB.Doctors.Practise.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ENB.Doctors.Practise.Entities.Repositories;
using ENB.Doctors.Practise.MVC.Models;
using Microsoft.CodeAnalysis.CSharp;

namespace ENB.Doctors.Practise.MVC.Controllers
{
    public class StaffPatientAssociationController : Controller
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

        public StaffPatientAssociationController( IAsyncPatientRepository asyncPatientRepository, 
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

        public async Task<IActionResult> StaffsperPatientList(int PatientId)
        {
            ViewBag.Idpatient = PatientId;
            var patient = await _asyncPatientRepository.FindById(PatientId);

            ViewBag.Message = patient.FullName;
            return View();
        }

        public async Task<IActionResult> GetStaffsperPatientList(int PatientId)

        {
            ViewBag.Idpatient = PatientId;

            var staffs = _asyncPatientRepository.FindAll().Where(l => l.Id == PatientId).SelectMany(l => l.Staff_Patient_Associations)
                                                      .Join(_asyncStaffRepository.FindAll(),
                                                      stpt => stpt.StaffId,
                                                      st => st.Id,
                                                      (staffpatient, staf) => new DisplayStaff_Patient_Association
                                                      {
                                                          Id = staffpatient.Id,   
                                                          NameStaff=staf.FullName,
                                                          Association_Start_Date = staffpatient.Association_Start_Date,
                                                          Association_End_Date=staffpatient.Association_End_Date,
                                                          DateCreated = staffpatient.DateCreated,
                                                          DateModified= staffpatient.DateModified,
                                                          Other_Details= staffpatient.Other_Details,
                                                      });




            var Mpdata = await Task.FromResult(_imapper.Map<List<DisplayStaff_Patient_Association>>(staffs));


            return Json(new { data = Mpdata });

        }

        public async Task<IActionResult> PatientsperStaffList(int StaffId)
        {
            ViewBag.Idstaff = StaffId;
            var staff = await _asyncStaffRepository.FindById(StaffId);

            ViewBag.Message = staff.FullName;
            return View();
        }
        public async Task<IActionResult> GetPatientsperStaffList(int StaffId)

        {
            ViewBag.Idstaff = StaffId;

            var patients = _asyncStaffRepository.FindAll().Where(l => l.Id == StaffId).SelectMany(l => l.Staff_Patient_Associations)
                                                      .Join(_asyncPatientRepository.FindAll(),
                                                      sfpt => sfpt.PatientId,
                                                      pt => pt.Id,
                                                      (staffpatient, pat) => new DisplayStaff_Patient_Association
                                                      {
                                                          Id = staffpatient.Id,
                                                          NamePatient=pat.FullName,
                                                          Association_Start_Date=staffpatient.Association_Start_Date,
                                                          Association_End_Date=staffpatient.Association_End_Date,
                                                          DateCreated = staffpatient.DateCreated,
                                                          DateModified= staffpatient.DateModified, 
                                                          Other_Details= staffpatient.Other_Details,                                                          
                                                          
                                                      });


            var pat = await _asyncStaffRepository.FindById(StaffId);

            ViewBag.Message = pat.FullName;

            var Mpdata = await Task.FromResult(_imapper.Map<List<DisplayStaff_Patient_Association>>(patients));

            return Json(new { data = Mpdata });
        }

        [HttpGet]
        public async Task<IActionResult> StaffsperPatientCreate(int PatientId)
        {
            ViewBag.Idpatient = PatientId;

            var data = new CreateAndEditStaff_Patient_Association()
            {
                ListStaff =  _asyncStaffRepository.FindAll()
                        .Select(d => new SelectListItem
                        {
                            Text = d.FullName,
                            Value = d.Id.ToString(),
                            Selected = true

                        }).Distinct().ToList()



            };

            var Nlawyer = await   _asyncPatientRepository.FindById(PatientId);

            ViewBag.Message = Nlawyer.FullName;

            return View(data);
        }


        public async Task<IActionResult> PatientsperStaffCreate(int StaffId)
        {
            ViewBag.Idstaff = StaffId;
            var data = new CreateAndEditStaff_Patient_Association()
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
        public async Task<IActionResult> StaffsperPatientCreate(CreateAndEditStaff_Patient_Association createAndEditStaff_Patient_Association,
                                                                  int PatientId)
        {
            ViewBag.PatientId = PatientId;
            if (ModelState.IsValid)
            {
                try
                {
                 await  using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var patient = await _asyncPatientRepository.FindById(PatientId);


                        var staffpatient = new Staff_Patient_Association();

                        ViewBag.Message = patient.FullName;
                    

                        _imapper.Map(createAndEditStaff_Patient_Association, staffpatient);
                       
                        patient.Staff_Patient_Associations.Add(staffpatient);

                        _notifyService.Success($"Staff related to Patient {ViewBag.Message} Successfully ");

                        return RedirectToAction(nameof(StaffsperPatientList), new { PatientId });
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
        public async Task<IActionResult> PatientsperStaffCreate(CreateAndEditStaff_Patient_Association createAndEditStaff_Patient_Association,
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

                        var staffpatient = new Staff_Patient_Association();

                        ViewBag.Message = staff.FullName;
                       

                        _imapper.Map(createAndEditStaff_Patient_Association, staffpatient);
                        
                        staff.Staff_Patient_Associations.Add(staffpatient);

                        _notifyService.Success($"Patient related to Staff {ViewBag.Message}  Successfully");

                        return RedirectToAction("PatientsperStaffList", new { StaffId });
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

        
        public async Task<IActionResult> StaffsperPatientEdit(int PatientId, int Id)
        {
            ViewBag.Idpatient = PatientId;
            ViewBag.Id = Id;

            var patient = await _asyncPatientRepository.FindById(PatientId);
            ViewBag.Message = patient.FullName;

            var staffpatient = await _asyncPatientRepository.FindById(PatientId, x => x.Staff_Patient_Associations);
            if (staffpatient == null)
            {
                return NotFound();
            }

            var data = new CreateAndEditStaff_Patient_Association()
            {

                ListStaff = _asyncStaffRepository.FindAll()
                        .Select(d => new SelectListItem
                        {
                            Text = d.FullName,
                            Value = d.Id.ToString(),
                            Selected = true

                        }).Distinct().ToList()


            };

            _imapper.Map(staffpatient.Staff_Patient_Associations.Single(x => x.Id == Id), data);

            return View(data);
        }

        public async Task<ActionResult> PatientsperStaffEdit(int StaffId, int id)
        {
            ViewBag.Idstaff = StaffId;
            ViewBag.Id = id;

            var staff = await _asyncStaffRepository.FindById(StaffId);
            ViewBag.Message = staff.FullName;

            var staffpatient = await _asyncStaffRepository.FindById(StaffId, x => x.Staff_Patient_Associations);

            if (staffpatient == null)
            {
                return NotFound();
            }


            var data = new CreateAndEditStaff_Patient_Association()
            {
                ListPatient = _asyncPatientRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.FullName,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()




            };
            _imapper.Map(staffpatient.Staff_Patient_Associations.Single(x => x.Id == id), data);




            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StaffsperPatientEdit(CreateAndEditStaff_Patient_Association createAndEditStaff_Patient_Association, 
                                                  int PatientId)
        {

            ViewBag.Idpatient = PatientId;
            if (ModelState.IsValid)
            {
                try
                {
                  await  using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var patientstaffs =await _asyncPatientRepository.FindById(PatientId, x => x.Staff_Patient_Associations);
                        var patientstaff = patientstaffs.Staff_Patient_Associations.Single(x => x.Id == createAndEditStaff_Patient_Association.Id);

                        _imapper.Map(createAndEditStaff_Patient_Association, patientstaff);

                        _notifyService.Success("Staff related to Patient  updated Successfully");

                        return RedirectToAction(nameof(StaffsperPatientList), new { PatientId });
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
        public async Task<ActionResult> PatientsperStaffEdit(CreateAndEditStaff_Patient_Association createAndEditStaff_Patient_Association,
                                                  int StaffId)
        {

            ViewBag.Idstaff = StaffId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var staffpatients = await _asyncStaffRepository.FindById(StaffId, x => x.Staff_Patient_Associations);
                        var staffpatient = staffpatients.Staff_Patient_Associations.Single(x => x.Id == createAndEditStaff_Patient_Association.Id);

                        _imapper.Map(createAndEditStaff_Patient_Association, staffpatient);

                        _notifyService.Success("patient related to staff update Successfully");

                        return RedirectToAction(nameof(PatientsperStaffList), new { StaffId });
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

        public async Task<ActionResult> StaffsperPatientDetails(int id, int PatientId)
        {

            ViewBag.Idpatient = PatientId;
            ViewBag.Id = id;

            var staffpatients = await _asyncPatientRepository.FindById(PatientId, x => x.Staff_Patient_Associations);
            var staffpatientsfil = staffpatients.Staff_Patient_Associations
                                                     .Join(_asyncStaffRepository.FindAll(),
                                                     staffpat => staffpat.StaffId,
                                                     st => st.Id,
                                                     (staffpatient, stf) => new DisplayStaff_Patient_Association
                                                     {
                                                         Id = staffpatient.Id,
                                                         NameStaff=stf.FullName,
                                                         Association_Start_Date=staffpatient.Association_Start_Date,
                                                         Association_End_Date=staffpatient.Association_End_Date,
                                                         Other_Details=staffpatient.Other_Details,
                                                         DateCreated = staffpatient.DateCreated,
                                                         DateModified = staffpatient.DateModified
                                                     }).ToList();


            var patient = await _asyncPatientRepository.FindById(PatientId);

            ViewBag.Message = patient.FullName;

            var sgl = staffpatientsfil.Single(x => x.Id == id);


            return View(sgl);
        }


        public async Task<ActionResult> PatientsperStaffDetails(int id, int StaffId)
        {

            ViewBag.Idstaff = StaffId;
            ViewBag.Id = id;

            var patientstaffs = await _asyncStaffRepository.FindById(StaffId, l => l.Staff_Patient_Associations);
            var patientstaffsfil = patientstaffs.Staff_Patient_Associations
                                                      .Join(_asyncPatientRepository.FindAll(),
                                                      stfpt => stfpt.PatientId,
                                                      pt => pt.Id,
                                                      (patientstaff, pat) => new DisplayStaff_Patient_Association
                                                      {
                                                          Id =patientstaff.Id,
                                                          NamePatient=pat.FullName,                                                          
                                                          Association_Start_Date=patientstaff.Association_Start_Date,
                                                          Association_End_Date=patientstaff.Association_End_Date,
                                                          DateCreated = patientstaff.DateCreated,
                                                          DateModified = patientstaff.DateModified,
                                                          Other_Details=patientstaff.Other_Details
                                                      }).ToList();


            var staff = await _asyncStaffRepository.FindById(StaffId);

            ViewBag.Message = staff.FullName;

            var sgl = patientstaffsfil.Single(x => x.Id == id);


            return View(sgl);
        }

        public async Task<ActionResult> StaffsperPatientDelete(int id, int PatientId)
        {

            ViewBag.Idpatient = PatientId;
            ViewBag.Id = id;

            var staffpatients = await _asyncPatientRepository.FindById(PatientId, x => x.Staff_Patient_Associations);
            var staffpatientsfil = staffpatients.Staff_Patient_Associations
                                                     .Join(_asyncStaffRepository.FindAll(),
                                                     staffpat => staffpat.StaffId,
                                                     st => st.Id,
                                                     (staffpatient, stf) => new DisplayStaff_Patient_Association
                                                     {
                                                         Id = staffpatient.Id,
                                                         NameStaff=stf.FullName,
                                                         Association_Start_Date = staffpatient.Association_Start_Date,
                                                         Association_End_Date = staffpatient.Association_End_Date,
                                                         Other_Details=staffpatient.Other_Details,
                                                         DateCreated = staffpatient.DateCreated,
                                                         DateModified = staffpatient.DateModified
                                                     }).ToList();


            var patient = await _asyncPatientRepository.FindById(PatientId);

            ViewBag.Message = patient.FullName;

            var sgl = staffpatientsfil.Single(x => x.Id == id);


            return View(sgl);
        }

        public async Task<ActionResult> PatientsperStaffDelete(int id, int StaffId)
        {

            ViewBag.Idstaff = StaffId;
            ViewBag.Id = id;

            var patientstaffs = await _asyncStaffRepository.FindById(StaffId, l => l.Staff_Patient_Associations);
            var patientstaffsfil = patientstaffs.Staff_Patient_Associations
                                                      .Join(_asyncPatientRepository.FindAll(),
                                                      stfpt => stfpt.PatientId,
                                                      pt => pt.Id,
                                                      (patientstaff, pat) => new DisplayStaff_Patient_Association
                                                      {
                                                          Id = patientstaff.Id,
                                                          NamePatient=pat.FullName,
                                                          Association_Start_Date = patientstaff.Association_Start_Date,
                                                          Association_End_Date = patientstaff.Association_End_Date,
                                                          DateCreated = patientstaff.DateCreated,
                                                          DateModified = patientstaff.DateModified,
                                                          Other_Details=patientstaff.Other_Details
                                                      }).ToList();


            var staff = await _asyncStaffRepository.FindById(StaffId);

            ViewBag.Message = staff.FullName;

            var sgl = patientstaffsfil.Single(x => x.Id == id);


            return View(sgl);
        }
        [HttpPost, ActionName("DeleteStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStafsperPatientConfirmed(DisplayStaff_Patient_Association displayStaff_Patient_Association, 
                                                    int PatientId)
        {
            ViewBag.Idpatient = PatientId;

          await  using (await _asyncUnitOfWorkFactory.Create())
            {
                var patientstaffs = await _asyncPatientRepository.FindById(PatientId, x => x.Staff_Patient_Associations);
                var patientstaff = patientstaffs.Staff_Patient_Associations.Single(x => x.Id == displayStaff_Patient_Association.Id);

                patientstaffs.Staff_Patient_Associations.Remove(patientstaff);

                _notifyService.Error("Staff related to Patient removed  Successfully");
            }
            return RedirectToAction(nameof(StaffsperPatientList), new { PatientId });
        }

        // POST: LawyersOnCasesController/Delete/5
        [HttpPost, ActionName("DeletePatient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePatientperStaffConfirmed(DisplayStaff_Patient_Association displayStaff_Patient_Association, 
                                                            int StaffId)
        {
            ViewBag.Idstaff = StaffId;
         await   using (await _asyncUnitOfWorkFactory.Create())
            {
                var staffpatients = await _asyncStaffRepository.FindById(StaffId, x => x.Staff_Patient_Associations);
                var staffpatient = staffpatients.Staff_Patient_Associations.Single(x => x.Id == displayStaff_Patient_Association.Id);
               
                staffpatients.Staff_Patient_Associations.Remove(staffpatient);
                _notifyService.Error("Patient related to Staff removed  Successfully");
            }
            return RedirectToAction(nameof(PatientsperStaffList), new { StaffId });
        }
    }
}
