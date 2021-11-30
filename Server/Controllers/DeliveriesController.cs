using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveriesController : ControllerBase
    {


        [HttpGet]
        public async Task<ActionResult<ICollection<Deliveries>>> GetAllDeliveries()
        {

        }
    }
}