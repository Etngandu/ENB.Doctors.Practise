using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENB.Doctors.Practise.Entities;
using ENB.Doctors.Practise.WebAPI.Models;

namespace ENB.Doctors.Practise.WebAPI.Help
{
    public class DoctorPractiseProfile : Profile
    {
        public DoctorPractiseProfile()
        {


            #region Patient 
            CreateMap<Patient, DisplayPatient>();

            CreateMap<CreateAndEditPatient, Patient>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.PatientAddress, t => t.Ignore());
            CreateMap<Patient, CreateAndEditPatient>();
            #endregion


            #region Staff
            CreateMap<Staff, DisplayStaff>();

            CreateMap<CreateAndEditStaff, Staff>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.StaffAddress, t => t.Ignore());
            CreateMap<Staff, CreateAndEditStaff>();

            #endregion

            //#region Identity
            //CreateMap<UserRegistrationModel, ApplicationUser>()
            //.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            //#endregion




            //#region PatientStaff
            //CreateMap<Staff_Patient_Association, DisplayStaff_Patient_Association>()
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditStaff_Patient_Association, Staff_Patient_Association>()
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ReverseMap();

            //#endregion

            //#region Appointment
            //CreateMap<Appointment, DisplayAppointment>()
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditAppointment, Appointment>()
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.Color, t => t.MapFrom(y => y.EventStatus.ToString()))
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore())
            //  .ReverseMap();
            //#endregion


            //#region PatientRecord
            //CreateMap<Patient_Record, DisplayPatientRecord>()
            // .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            // .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            // .ForMember(d => d.Staff, t => t.Ignore())
            // .ForMember(d => d.Patient, t => t.Ignore());

            //CreateMap<CreateAndEditPatientRecord, Patient_Record>()
            //  .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
            //  .ForMember(d => d.PatientId, t => t.MapFrom(y => y.PatientId))
            //  .ForMember(d => d.Staff, t => t.Ignore())
            //  .ForMember(d => d.Patient, t => t.Ignore())
            //  .ReverseMap();

            //#endregion
            //#region BookingNotes
            //CreateMap<Booking_Note, DisplayBookingNote>()
            // .ForMember(d => d.Customer, t => t.Ignore())
            // .ForMember(d => d.Booking, t => t.Ignore())            
            // .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
            // .ForMember(d => d.BookingId, t => t.MapFrom(y => y.BookingId));

            //CreateMap<CreateAndEditBookingNote, Booking_Note>()
            //  .ForMember(d => d.BookingId, t => t.MapFrom(y => y.BookingId))            
            //  .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
            //  .ForMember(d => d.Customer, t => t.Ignore())
            //  .ForMember(d => d.Booking, t => t.Ignore())              
            //  .ForMember(d => d.DateCreated, t => t.Ignore())
            //  .ForMember(d => d.DateModified, t => t.Ignore())
            //  .ReverseMap();

            //#endregion

            //#region Address

            //CreateMap<Address, EditAddress>()
            //      .ForMember(d => d.CustomerId, t => t.Ignore())
            //      .ForMember(d => d.StaffId, t => t.Ignore());                  
            //CreateMap<EditAddress, Address>().ConstructUsing(s => new Address(s.Number_street!, s.City!, s.Zipcode!, s.State_province_county!, s.Country!));
            //#endregion
        }
    }
}
