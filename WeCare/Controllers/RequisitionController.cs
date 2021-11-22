using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Service;
using WeCare.Util;

namespace WeCare.Controllers
{
    [Route("requisitions")]
    [ApiController]
    public class RequisitionController : ControllerBase
    {
        private readonly RequisitionService pRequisitionService;
        public RequisitionController(RequisitionService requisitionService)
        {
            pRequisitionService = requisitionService;
        }

        [HttpGet]
        public ActionResult<DataCollection<RequisitionDto>> GetAll(int page = 1, int take = 20)
        {
            return pRequisitionService.GetAll(page, take);
        }

        [HttpGet("simple")]
        public ActionResult<DataCollection<RequisitionSimpleDto>> GetAllSimple(int page = 1, int take = 20)
        {
            return pRequisitionService.GetAllSimple(page, take);
        }

        [HttpGet("simple/patient/{patient_id}")]
        public ActionResult<DataCollection<RequisitionSimpleDto>> GetSimpleByPatientId(int patient_id, int page = 1, int take = 20)
        {
            return pRequisitionService.GetSimpleByPatientId(patient_id, page, take);
        }
        [HttpGet("simple/specialist/{specialist_id}")]
        public ActionResult<DataCollection<RequisitionSimpleDto>> GetSimpleBySpecialistId(int specialist_id, int page = 1, int take = 20)
        {
            return pRequisitionService.GetSimpleBySpecialistId(specialist_id, page, take);
        }
        [HttpGet("{id}")]
        public ActionResult<RequisitionDto> GetById(int id)
        {
            return pRequisitionService.GetById(id);
        }

        [HttpPost]
        public ActionResult Create(RequisitionCreateDto requisition) // Careful: "event" is a reserved word
        {
            var rs = pRequisitionService.Create(requisition);
            if (rs.RequisitionId != 0) return Ok(JObject.Parse("{requisitionId:" + rs.RequisitionId + "}"));
            else return BadRequest(JObject.Parse("{requisitionId:" + rs.RequisitionId + "}"));
        }
        // Falta agregar update
    }
}
