
using AutoMapper;
using ENB.Doctors.Practise.Entities;
using ENB.Doctors.Practise.Entities.Repositories;
using ENB.Doctors.Practise.Infrastructure;
using ENB.Doctors.Practise.MVC.Help;
using ENB.Doctors.Practise.WebAPI.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;



namespace ENB.Doctors.Practise.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PatientController> _logger;
        private readonly IAsyncPatientRepository _asyncPatientRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;        
        private readonly IValidator<CreateAndEditPatient> _validator;
        public PatientController( IMapper mapper, ILogger<PatientController> logger,
                                   IAsyncPatientRepository asyncPatientRepository, 
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,                                  
                                   IValidator<CreateAndEditPatient> validator)
        {
            _mapper = mapper;
            _logger = logger;   
            _asyncPatientRepository = asyncPatientRepository;
            _asyncUnitOfWorkFactory= asyncUnitOfWorkFactory;           
            _validator = validator;
        }

        // GET: PatientController

        [HttpGet]
        [Route("AllPatients")]
        public async Task<IActionResult> GetPatientData()
        {
            IQueryable<Patient> allPatient = _asyncPatientRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayPatient>>(allPatient).ToList();
            await Task.FromResult(Mpdata);
            return Ok(Mpdata);
        }

        // GET: LawyerController/Details/5
        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
           

            _logger.LogError($"Id :{id} of Patient not found");

            Patient dbPatient = await _asyncPatientRepository.FindById(id);

            

            _logger.LogInformation($"Details of Patient: {id}");

            if (dbPatient is null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplayPatient>(dbPatient);

            return Ok(data);
        }



        // POST: PatientController/Create
        [HttpPost]
        [Route("createpatient")]
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
               // return View("Create", createAndEditPatient);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Patient dbPatient = new();

                    _mapper.Map(createAndEditPatient, dbPatient);
                    await _asyncPatientRepository.Add(dbPatient);

                 

                    return Ok(createAndEditPatient);
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
            return Ok(createAndEditPatient);
        }

        [HttpGet]
        [Route("retrievepatient/{id}")]
        public async Task<IActionResult> RetrievePatient(int id)
        {

            _logger.LogError($"Patient {id} not found");
           
            Patient dbPatient = await _asyncPatientRepository.FindById(id);

            if (dbPatient is null)
            {
                return NotFound();
            }
            var data = await Task.FromResult(_mapper.Map<CreateAndEditPatient>(dbPatient));

            return Ok(data);
        }

        // POST: PatientController/Edit/5
        [HttpPut]
        [Route("editpatient")]
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
               // return View("Edit", createAndEditPatient);
            }
            else
            {
                await using (await _asyncUnitOfWorkFactory.Create())
                {

                    Patient dbPatientToUpdate = await _asyncPatientRepository.FindById(createAndEditPatient.Id);

                    _mapper.Map(createAndEditPatient, dbPatientToUpdate, typeof(CreateAndEditPatient), typeof(Patient));

                    

                    return Ok(createAndEditPatient);
                }
            }
            return Ok(createAndEditPatient);
        }


        [HttpDelete]
        [Route("deletepatient/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Patient dbPatient = await _asyncPatientRepository.FindById(id);
         await using (await _asyncUnitOfWorkFactory.Create())
            {
                 _asyncPatientRepository.Remove(dbPatient);

                 
            }

            return Ok(); 
        }
    }
}
