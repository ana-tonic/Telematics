using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveriesController : ControllerBase
    {
        public DataProvider data { get; set; }

        public DeliveriesController()
        {
            data = new DataProvider();
        }

        [HttpGet]
        [Route("GetAllDeliveries")]
        public IActionResult GetAllDeliveries()
        {
            return new JsonResult(data.getAllDeliveries());
        }

        [HttpPost]
        [Route("CreateDelivery")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateDelivery([FromBody] Deliveries d)
        {
            data.CreateDelivery(d);
            return Ok();
        }
    }
}