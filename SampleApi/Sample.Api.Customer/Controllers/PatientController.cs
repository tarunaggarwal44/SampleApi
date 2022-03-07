using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Customer.Controllers
{
    //[Route("api/v1/customers")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : BaseApiController
    {

        //GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(this.Ok("Hello"));
        }


      
    }
}
