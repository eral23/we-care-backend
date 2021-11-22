using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Entities;

namespace WeCare.Persistance.Config
{
    public class StateConfig
    {
        public StateConfig(EntityTypeBuilder<State> entityBuilder)
        {
            entityBuilder.Property(x => x.StateId).IsRequired();
            entityBuilder.Property(x => x.StateBPM).IsRequired();
            entityBuilder.Property(x => x.StateSystolicPressure).IsRequired();
            entityBuilder.Property(x => x.StateDiastolicPressure).IsRequired();
            entityBuilder.Property(x => x.StateDate).IsRequired();
            entityBuilder.Property(x => x.StateTime).IsRequired();
            entityBuilder.HasOne(x => x.Patient).WithMany(x => x.States).HasForeignKey(x => x.PatientId);
        }
    }
}
