using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveriesController
    {
      

        [HttpGet]
        public async Task<ActionResult<ICollection<Deliveries>>> GetAllDeliveries()
        {   
            
        }
    }
}