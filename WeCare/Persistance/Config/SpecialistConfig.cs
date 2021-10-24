using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Entities;

namespace WeCare.Persistance.Config
{
    public class SpecialistConfig
    {
        public SpecialistConfig(EntityTypeBuilder<Specialist> entityBuilder)
        {
            entityBuilder.Property(x => x.SpecialistId).IsRequired();
            entityBuilder.Property(x => x.SpecialistName).IsRequired();
            entityBuilder.Property(x => x.SpecialistLastname).IsRequired();
            entityBuilder.Property(x => x.SpecialistEmail).IsRequired();
        }
    }
}
