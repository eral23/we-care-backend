using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Service;
using WeCare.Util;

namespace WeCare.Controllers
{
    [Route("patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService pPatientService;
        public PatientController(PatientService patientService)
        {
            pPatientService = patientService;
        }

        [HttpGet]
        public ActionResult<DataCollection<PatientDto>> GetAll(int page = 1, int take = 20)
        {
            return pPatientService.GetAll(page, take);
        }

        [HttpGet("simple")]
        public ActionResult<DataCollection<PatientSimpleDto>> GetAllSimple(int page = 1, int take = 20)
        {
            return pPatientService.GetAllSimple(page, take);
        }

        [HttpGet("simple/{specialist_id}")]
        public ActionResult<DataCollection<PatientSimpleDto>> GetSimpleBySpecialistId(int specialist_id, int page = 1, int take = 20)
        {
            return pPatientService.GetSimpleBySpecialistId(specialist_id, page, take);
        }

        [HttpGet("{id}")]
        public ActionResult<PatientDto> GetById(int id)
        {
            return pPatientService.GetById(id);
        }
        [HttpGet("search/{email}")]
        public ActionResult<PatientDto> GetByEmail(string email)
        {
            return pPatientService.GetByEmail(email);
        }

        [HttpPost]
        public ActionResult Create(PatientCreateDto patient)
        {
            pPatientService.Create(patient);
            return Ok();
        }
    }
}
