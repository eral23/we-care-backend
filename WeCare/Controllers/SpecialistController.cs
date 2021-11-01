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
    [Route("specialists")]
    [ApiController]
    public class SpecialistController : ControllerBase
    {
        private readonly SpecialistService pSpecialistService;
        public SpecialistController(SpecialistService specialistService)
        {
            pSpecialistService = specialistService;
        }
        
        [HttpGet]
        public ActionResult<DataCollection<SpecialistDto>> GetAll(int page = 1, int take = 20)
        {
            return pSpecialistService.GetAll(page, take);
        }

        [HttpGet("simple")]
        public ActionResult<DataCollection<SpecialistSimpleDto>> GetAllSimple(int page = 1, int take = 20)
        {
            return pSpecialistService.GetAllSimple(page, take);
        }

        [HttpGet("{id}")]
        public ActionResult<SpecialistDto> GetById(int id)
        {
            return pSpecialistService.GetById(id);
        }

        [HttpPost]
        public ActionResult Create(SpecialistCreateDto specialist)
        {
            pSpecialistService.Create(specialist);
            return Ok();
        }
    }
}
