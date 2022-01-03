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
        [Route("GetDeliveries/{cargo}&{year}")]
        public IActionResult GetDeliveries(string cargo, int year)
        {
            return new JsonResult(data.getDeliveries(cargo, year));
        }

        [HttpDelete]
        [Route("DeleteDelivery/{cargo}&{year}&{delivery_id}")]
        public IActionResult GetDeliveries(string cargo, int year, string delivery_id)
        {
            data.DeleteDelivery(cargo, year, Guid.Parse(delivery_id));
            return Ok();
        }

        [HttpPost]
        [Route("CreateDelivery")]
        public IActionResult CreateDelivery([FromBody] Deliveries d)
        {
            d.Delivery_Id = TimeUuid.NewId();
            d.Active = true;
            d.Year = DateTime.Now.Year;
            d.Departing_Time = DateTimeOffset.Now;
            data.CreateDelivery(d);
            data.StartDelivery(d);
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
        [Route("CreateFuel/")]
        public IActionResult CreateFuel([FromBody]Vehicle_fuel fuel)
        {
            data.CreateFuel(fuel);
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
        [Route("CreateLocation/")]
        public IActionResult CreateLocation([FromBody] Vehicle_location loc)
        {
            data.CreateLocation(loc);
            return Ok();
        }

        #endregion

        #region speed

        [HttpGet]
        [Route("GetSpeed/{delivery_id}")]
        public IActionResult GetSpeed(string delivery_id)
        {
            return new JsonResult(data.getSpeed(Guid.Parse(delivery_id)));
        }

        [HttpPost]
        [Route("CreateSpeed/")]
        public IActionResult CreateSpeed([FromBody] Vehicle_speed speed)
        {
            data.CreateSpeed(speed);
            return Ok();
        }

        #endregion

        #region idling

        [HttpGet]
        [Route("GetIdling/{delivery_id}")]
        public IActionResult GetIdling(string delivery_id)
        {
            return new JsonResult(data.getIdling(Guid.Parse(delivery_id)));
        }

        [HttpPost]
        [Route("CreateIdling/")]
        public IActionResult CreateIdling([FromBody] Vehicle_idling_time idle)
        {
            data.CreateIdling(idle);
            return Ok();
        }
        #endregion

    }
}