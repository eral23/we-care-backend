using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Util;

namespace WeCare.Service
{
    public interface SpecialistService
    {
        DataCollection<SpecialistDto> GetAll(int page, int take);
        DataCollection<SpecialistSimpleDto> GetAllSimple(int page, int take);
        SpecialistDto GetById(int specialistId);
        SpecialistDto Create(SpecialistCreateDto model);
        SpecialistSimpleDto GetByEmail(string specialistEmail);
        // Change Status of relationship with patient: TBD
    }
}
