using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController
    {
        [HttpGet]
        public string Index()
        {
            return "Good news, API running...";
        }
    }
}
