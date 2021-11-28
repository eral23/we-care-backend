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
        // Metodos de filtrado
        // Eventos hoy
        List<EventSimpleDto> GetTodayEvents(int patientId, int page, int take);
        // Eventos de la semana, empezando desde el lunes siempre
        List<(string, int)> GetWeeklyEvents(int patientId, int page, int take);
        // Eventos del mes, empezando desde el día 1 siempre
        List<(string, int)> GetMonthlyEvents(int patientId, int page, int take);

    }
}
