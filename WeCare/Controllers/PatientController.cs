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

        [HttpGet("{id}")]
        public ActionResult<PatientDto> GetById(int id)
        {
            var res = pPatientService.GetById(id);
            if (res.PatientName != null) return res;
            else return BadRequest(res);
        }
        [HttpGet("search/{email}")]
        public ActionResult<PatientSimpleDto> GetByEmail(string email)
        {
            var res = pPatientService.GetByEmail(email);
            if (res.PatientName != null) return res;
            else return BadRequest(res);
        }

        //[HttpPost]
        //public ActionResult Create(PatientCreateDto patient)
        //{
        //    pPatientService.Create(patient);
        //    return Ok();
        //}
    }
}
