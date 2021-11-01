using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Util;

namespace WeCare.Service
{
    public interface PatientService
    {
        DataCollection<PatientDto> GetAll(int page, int take);
        DataCollection<PatientSimpleDto> GetAllSimple(int page, int take);
        DataCollection<PatientSimpleDto> GetSimpleBySpecialistId(int specialistId, int page, int take);
        PatientDto GetById(int patientId);
        PatientDto GetByEmail(string patientEmail);
        PatientDto Create(PatientCreateDto model);
    }
}
