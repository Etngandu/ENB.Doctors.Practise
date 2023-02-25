using System.ComponentModel.DataAnnotations;
using System.Data;
using ENB.Restaurant.Event.Bookings.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using ENB.Doctors.Practise.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ENB.Doctors.Practise.EF.ConfigurationEntity;
using ENB.Doctors.Practise.Entities;

namespace ENB.Doctors.Practise.EF
{
    /// <summary>
    /// This is the main DbContext to work with data in the database.
    /// </summary>
    public class DoctorPractiseContext : IdentityDbContext<ApplicationUser>
    {
       

        public DoctorPractiseContext(DbContextOptions<DoctorPractiseContext>  options)
               :base(options)
        {
            
        }

        public DbSet<Patient>? Patients { get; set; }
        public DbSet<Staff>? Staffs { get; set; }        
      




        /// <summary>
        /// Hooks into the Save process to get a last-minute chance to look at the entities and change them. Also intercepts exceptions and 
        /// wraps them in a new Exception type.
        /// </summary>
        /// <returns>The number of affected rows.</returns>
       

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            

            
            try
            {
                var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
                foreach (EntityEntry item in modified)
                {
                    var changedOrAddedItem = item.Entity as IDateTracking;
                    if (changedOrAddedItem != null)
                    {
                        if (item.State == EntityState.Added)
                        {
                            changedOrAddedItem.DateCreated = DateTime.Now;
                        }
                        changedOrAddedItem.DateModified = DateTime.Now;
                    }
                    var valProvider = new ValidationDbContextServiceProvider(this);
                    var validationContext = new ValidationContext(item, valProvider, null);
                    Validator.ValidateObject(item, validationContext);
                }
                // return base.SaveChanges();
            }
            catch (Exception )
            {

               // throw new ModelValidationException(result.ToString(), entityException, allErrors);
                //var exStatus = SaveChangesExtensions.SaveChangesExceptionHandler(e, this);
                //if (exStatus == null) throw;       //error wasn't handled, so rethrow
                //status.CombineStatuses(exStatus);
            }
            return base.SaveChangesAsync(cancellationToken);
        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }



    }
}