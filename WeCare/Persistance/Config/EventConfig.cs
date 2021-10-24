using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Entities;

namespace WeCare.Persistance.Config
{
    public class EventConfig
    {
        public EventConfig(EntityTypeBuilder<Event> entityBuilder)
        {
            entityBuilder.Property(x => x.EventId).IsRequired();
            entityBuilder.Property(x => x.EventName).IsRequired();
            entityBuilder.Property(x => x.EventDescription).IsRequired();
            entityBuilder.Property(x => x.EventScore).IsRequired();
            entityBuilder.Property(x => x.EventResult).IsRequired();
            entityBuilder.Property(x => x.EventDetail).IsRequired();
            entityBuilder.Property(x => x.EventDate).IsRequired();
            entityBuilder.Property(x => x.EventTime).IsRequired();
            entityBuilder.HasOne(x => x.Patient).WithMany(x => x.Events).HasForeignKey(x => x.PatientId);
        }
    }
}
