using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ENB.Doctors.Practise.Entities;
using ENB.Doctors.Practise.Entities.Repositories;
using ENB.Doctors.Practise.Infrastructure;
using ENB.Doctors.Practise.MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace ENB.Doctors.Practise.MVC.Controllers
{
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly IAsyncPatientRepository _asyncPatientRepository;
        private readonly IAsyncStaffRepository _asyncStaffRepository;        
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;


        /// <summary>
        /// Initializes a new instance of the AddressesController class.
        /// </summary>
        public AddressesController(IAsyncPatientRepository asyncPatientRepository,
                                   IAsyncStaffRepository asyncStaffRepository,                                   
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   IMapper mapper,
                                   INotyfService notyf)
        {
            _asyncPatientRepository = asyncPatientRepository;           
            _asyncStaffRepository = asyncStaffRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> Edit(int PatientId, int StaffId)
        {
            ViewBag.PatientId = PatientId;
            ViewBag.StaffId = StaffId;
           
            
            var address = new Address();
            var message = "";

            if (PatientId != 0)
            {
                var Patient = await _asyncPatientRepository.FindById(PatientId);
                if (Patient == null)
                {
                    return NotFound();
                }
                address = Patient.PatientAddress;
                message = Patient.FullName;
            }

            if (StaffId != 0)
            {
                var staff = await _asyncStaffRepository.FindById(StaffId);
                if (staff is null)
                {
                    return NotFound();
                }
                address = staff.StaffAddress;
                message = staff.FullName;
            }

            

            var data = new EditAddress();

            ViewBag.Message = message;

            _mapper.Map(address, data);
            return View(data);
        }

        public  IActionResult Redirect(int PatientId, int StaffId)
        {
            ViewBag.PatientId = PatientId;
            ViewBag.StaffId = StaffId;
            var redirect= RedirectToAction("");

        //  private ActionResult result { get; set; }=ActionResult.
            

            if (PatientId != 0)
            {
              redirect=  RedirectToAction("Index","Patient");
            }

            if (StaffId != 0)
            {
              redirect=  RedirectToAction("Index","Staff");
            }


            return redirect;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAddress editAddressModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        if (editAddressModel.PatientId != 0)
                        {
                            var Patient = await _asyncPatientRepository.FindById(editAddressModel.PatientId);
                            _mapper.Map(editAddressModel, Patient.PatientAddress);

                            _notyf.Success("Address created  Successfully! ");

                            return RedirectToAction(nameof(Index), "Patient");
                        }

                        if (editAddressModel.StaffId != 0)
                        {
                            var staff = await _asyncStaffRepository.FindById(editAddressModel.StaffId);
                            _mapper.Map(editAddressModel, staff.StaffAddress);

                            _notyf.Success("Address created  Successfully! ");

                            return RedirectToAction(nameof(Index), "Staff");
                        }                       


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
    }
}
