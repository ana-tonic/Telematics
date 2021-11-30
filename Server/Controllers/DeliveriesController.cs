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


        [HttpGet]
        [Route("GetAllDeliveries")]
        public string GetAllDeliveries()
        {
            return DataProvider.getAllDeliveries();
        }
    }
}