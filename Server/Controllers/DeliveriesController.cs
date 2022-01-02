using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
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
        public IActionResult CreateDelivery([FromBody] Deliveries d)
        {

            data.CreateDelivery(d);
            return Ok();
        }

        #endregion Deliveries

        #region fuel

        [HttpGet]
        [Route("GetFuel/{delivery_id}")]
        public IActionResult GetFuel(string delivery_id)
        {
            return new JsonResult(data.getFuel(Guid.Parse(delivery_id)));
        }

        [HttpPost]
        [Route("CreateFuel/{delivery_id}")]
        public IActionResult CreateFuel(string delivery_id)
        {
            data.CreateFuel(Guid.Parse(delivery_id));
            return Ok();
        }

        #endregion

        #region location

        [HttpGet]
        [Route("GetLocation/{delivery_id}")]
        public IActionResult GetLocation(string delivery_id)
        {
            return new JsonResult(data.getLocation(Guid.Parse(delivery_id)));
        }

        [HttpPost]
        [Route("CreateLocation/{delivery_id}")]
        public IActionResult CreateLocation(string delivery_id)
        {
            data.CreateLocation(Guid.Parse(delivery_id));
            return Ok();
        }

        #endregion

        #region speed

        [HttpGet]
        [Route("GetSpeed/{delivery_id}")]
        public IActionResult GetSpeed(Cassandra.TimeUuid delivery_id)
        {
            return new JsonResult(data.getSpeed(delivery_id));
        }

        #endregion

        #region idling

        [HttpGet]
        [Route("GetIdling/{delivery_id}")]
        public IActionResult GetIdling(Cassandra.TimeUuid delivery_id)
        {
            return new JsonResult(data.getIdling(delivery_id));
        }

        #endregion

    }
}