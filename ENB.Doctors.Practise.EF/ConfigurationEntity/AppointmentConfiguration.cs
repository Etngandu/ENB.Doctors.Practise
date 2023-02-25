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
   public class AppointmentConfiguration:IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(x => x.Other_details).IsRequired().HasMaxLength(350);
            builder.Property(x => x.Color).IsRequired().HasMaxLength(150);

        }
    }
}
