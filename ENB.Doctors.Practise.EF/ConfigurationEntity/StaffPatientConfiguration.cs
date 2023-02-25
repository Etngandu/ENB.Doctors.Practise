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
   public class StaffPatientConfiguration : IEntityTypeConfiguration<Staff_Patient_Association>
    {
        public void Configure(EntityTypeBuilder<Staff_Patient_Association> builder)
        {
            builder.Property(x => x.Other_Details).IsRequired().HasMaxLength(350);           

        }
    }
}
