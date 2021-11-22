using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Entities;

namespace WeCare.Persistance.Config
{
    public class RequisitionConfig
    {
        public RequisitionConfig(EntityTypeBuilder<Requisition> entityBuilder)
        {
            entityBuilder.Property(x => x.RequisitionId).IsRequired();
            entityBuilder.Property(x => x.RequisitionStatus).IsRequired();
            entityBuilder.HasOne(x => x.Patient).WithMany(x => x.Requisitions).HasForeignKey(x => x.PatientId);
            entityBuilder.HasOne(x => x.Specialist).WithMany(x => x.Requisitions).HasForeignKey(x => x.SpecialistId);
        }
    }
}
