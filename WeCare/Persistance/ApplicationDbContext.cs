using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Entities;
using WeCare.Persistance.Config;

namespace WeCare.Persistance
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // Falta definir login
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<Event> Events { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new PatientConfig(builder.Entity<Patient>());
            new SpecialistConfig(builder.Entity<Specialist>());
            new EventConfig(builder.Entity<Event>());
        }
    }
}
