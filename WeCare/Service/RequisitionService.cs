using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Util;

namespace WeCare.Service
{
    public interface RequisitionService
    {
        DataCollection<RequisitionDto> GetAll(int page, int take);
        DataCollection<RequisitionSimpleDto> GetAllSimple(int page, int take);
        DataCollection<RequisitionSimpleDto> GetSimpleByPatientId(int patientId, int page, int take);
        DataCollection<RequisitionSimpleDto> GetSimpleBySpecialistId(int specialistId, int page, int take);
        RequisitionDto GetById(int requisitionId);
        RequisitionDto Create(RequisitionCreateDto model);
        RequisitionDto Update(RequisitionUpdateDto model);
    }
}
