using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Doctors.Practise.Entities;
using ENB.Doctors.Practise.Entities.Repositories;
using ENB.Doctors.Practise.Infrastructure;
using ENB.Doctors.Practise.MVC.Help;
using ENB.Doctors.Practise.MVC.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;



namespace ENB.Doctors.Practise.MVC.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PatientController> _logger;
        private readonly IAsyncPatientRepository _asyncPatientRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        private readonly IValidator<CreateAndEditPatient> _validator;
        public PatientController( IMapper mapper, ILogger<PatientController> logger,
                                   IAsyncPatientRepository asyncPatientRepository, 
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf,
                                   IValidator<CreateAndEditPatient> validator)
        {
            _mapper = mapper;
            _logger = logger;   
            _asyncPatientRepository = asyncPatientRepository;
            _asyncUnitOfWorkFactory= asyncUnitOfWorkFactory;
            _notyf = notyf;
            _validator = validator;
        }

        // GET: PatientController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetPatientData()
        {
            IQueryable<Patient> allPatient = _asyncPatientRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayPatient>>(allPatient).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // GET: PatientController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Id = id;

            _logger.LogError($"Id :{id} of Patient not found");

            Patient dbPatient = await _asyncPatientRepository.FindById(id);

            ViewBag.Message = dbPatient.FullName;

            _logger.LogInformation($"Details of Patient: {ViewBag.Message}");

            if (dbPatient is null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplayPatient>(dbPatient);

            return View(data);
        }

        // GET: PatientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Create(CreateAndEditPatient createAndEditPatient )
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            
            ValidationResult result = await _validator.ValidateAsync(createAndEditPatient);


            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Create", createAndEditPatient);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Patient dbPatient = new();

                    _mapper.Map(createAndEditPatient, dbPatient);
                    await _asyncPatientRepository.Add(dbPatient);

                    _notyf.Success("Patient Created  Successfully! ");

                    return RedirectToAction("Index");
                }
            }
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        await using (await _asyncUnitOfWorkFactory.Create())
            //        {

            //            Patient dbPatient = new(); 

            //            _mapper.Map(createAndEditPatient, dbPatient);
            //            await _asyncPatientRepository.Add(dbPatient);

            //            _notyf.Success("Patient Created  Successfully! ");

            //            return RedirectToAction("Index");
            //        }
            //    }
            //    catch (ModelValidationException mvex)
            //    {
            //        foreach (var error in mvex.ValidationErrors)
            //        {
            //            ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
            //        }
            //        // result.AddToModelState(this.ModelState);
            //    }
            //}
            //return View();
        }

        // GET: PatientController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            _logger.LogError($"Patient {id} not found");
           
            Patient dbPatient = await _asyncPatientRepository.FindById(id);

            if (dbPatient is null)
            {
                return NotFound();
            }
            var data = await Task.FromResult(_mapper.Map<CreateAndEditPatient>(dbPatient));

            return View(data);
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditPatient createAndEditPatient)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        await using (await _asyncUnitOfWorkFactory.Create())
            //        {

            //            Patient dbPatientToUpdate = await _asyncPatientRepository.FindById(createAndEditPatient.Id);

            //            _mapper.Map(createAndEditPatient, dbPatientToUpdate, typeof(CreateAndEditPatient), typeof(Patient));

            //             _notyf.Success("Patient Update  Successfully! ");

            //            return RedirectToAction(nameof(Index));
            //        }
            //    }
            //    catch (ModelValidationException mvex)
            //    {
            //        foreach (var error in mvex.ValidationErrors)
            //        {
            //            ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
            //        }
            //    }
            //}
            //return View();
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ValidationResult result = await _validator.ValidateAsync(createAndEditPatient);

            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Edit", createAndEditPatient);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Patient dbPatientToUpdate = await _asyncPatientRepository.FindById(createAndEditPatient.Id);

                    _mapper.Map(createAndEditPatient, dbPatientToUpdate, typeof(CreateAndEditPatient), typeof(Patient));

                    _notyf.Success("Patient Update  Successfully! ");

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // GET: PatientController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Patient dbPatient = await _asyncPatientRepository.FindById(id);
            ViewBag.Message = dbPatient.FullName;

            if (dbPatient is null)
            {
                return NotFound();
            }
            var data = _mapper.Map<DisplayPatient>(dbPatient);
            return View(data);
        }

        // POST: PatientController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Patient dbPatient = await _asyncPatientRepository.FindById(id);
         await using (await _asyncUnitOfWorkFactory.Create())
            {
                 _asyncPatientRepository.Remove(dbPatient);

                  _notyf.Error("Patient Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
