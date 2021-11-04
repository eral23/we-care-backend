using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Util;

namespace WeCare.Service
{
    public interface EventService
    {
        DataCollection<EventDto> GetAll(int page, int take);
        DataCollection<EventSimpleDto> GetAllSimple(int page, int take);
        DataCollection<EventSimpleDto> GetSimpleByPatientId(int patientId, int page, int take);
        EventDto GetById(int eventId);
        EventDto Create(EventCreateDto model);
        EventDto CreateSimple(EventSimpleCreateDto model);
    }
}
