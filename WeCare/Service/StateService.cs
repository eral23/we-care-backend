using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Util;

namespace WeCare.Service
{
    public interface StateService
    {
        DataCollection<StateDto> GetAll(int page, int take);
        DataCollection<StateSimpleDto> GetAllSimple(int page, int take);
        DataCollection<StateSimpleDto> GetSimpleByPatientId(int patientId, int page, int take);
        StateDto GetById(int stateId);
        StateDto Create(StateCreateDto model);
        StateDto CreateSimple(StateSimpleCreateDto model);
        // Metodos de filtrado
    }
}
