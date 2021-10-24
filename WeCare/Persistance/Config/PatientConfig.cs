using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Entities;

namespace WeCare.Persistance.Config
{
    public class PatientConfig
    {
        public PatientConfig(EntityTypeBuilder<Patient> entityBuilder)
        {
            entityBuilder.Property(x => x.PatientId).IsRequired();
            entityBuilder.Property(x => x.PatientName).IsRequired();
            entityBuilder.Property(x => x.PatientLastname).IsRequired();
            entityBuilder.Property(x => x.PatientEmail).IsRequired();
            entityBuilder.HasOne(x => x.Specialist).WithMany(x => x.Patients).HasForeignKey(x => x.SpecialistId);
        }
    }
}
