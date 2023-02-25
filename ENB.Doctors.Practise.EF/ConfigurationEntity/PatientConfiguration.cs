using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ENB.Doctors.Practise.Infrastructure;
using ENB.Doctors.Practise.Entities;

namespace ENB.Doctors.Practise.EF.ConfigurationEntity
{
   public class PatientConfiguration:IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(x => x.Patient_first_name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Patient_last_name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Patient_mailAddress).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Cell_Mobile_phone).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Next_of_kin).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Height).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Hospital_Number).IsRequired().HasMaxLength(50);
            builder.Property(x => x.nhs_number).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Weight).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Height).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Other_patient_details).IsRequired().HasMaxLength(300);
            builder.OwnsOne(o => o.PatientAddress, a =>
            {
                a.Property(p => p.Number_street).HasMaxLength(600)
                    .HasColumnName("Numberstreet")
                    .HasDefaultValue("");
                a.Property(p => p.City).HasMaxLength(250)
                    .HasColumnName("City")
                    .HasDefaultValue("");
                a.Property(p => p.State_province_county).HasMaxLength(250)
                    .HasColumnName("State_province_county")
                    .HasDefaultValue("");
                a.Property(p => p.Zipcode).HasMaxLength(12)
                    .HasColumnName("ZipCode")
                    .HasDefaultValue("");
                a.Property(p => p.Country).HasMaxLength(250)
                   .HasColumnName("Country")
                   .HasDefaultValue("");
            });

        }
    }
}
