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
    #region Deliveries

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

        [HttpGet]
        [Route("GetDeliveries/{cargo}&{year}")]
        public IActionResult GetDeliveries(string cargo, int year)
        {
            return new JsonResult(data.getDeliveries(cargo, year));
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

        #endregion Deliveries

        #region speed

        [HttpGet]
        [Route("GetFuel/{delivery_id}")]
        public IActionResult GetFuel(Cassandra.TimeUuid delivery_id)
        {
            return new JsonResult(data.getFuel(delivery_id));
        }


        [HttpGet]
        [Route("GetLocation/{delivery_id}")]
        public IActionResult GetLocation(Cassandra.TimeUuid delivery_id)
        {
            return new JsonResult(data.GetLocation(delivery_id));
        }

        [HttpGet]
        [Route("GetSpeed/{delivery_id}")]
        public IActionResult GetSpeed(Cassandra.TimeUuid delivery_id)
        {
            return new JsonResult(data.GetSpeed(delivery_id));
        }

        [HttpGet]
        [Route("GetIdling/{delivery_id}")]
        public IActionResult GetIdling(Cassandra.TimeUuid delivery_id)
        {
            return new JsonResult(data.GetIdling(delivery_id));
        }

        #endregion
    }
}