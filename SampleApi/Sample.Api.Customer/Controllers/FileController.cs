using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Common;
using Sample.Api.Common.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Customer.Controllers
{
    [ApiController]
    public class FileController : BaseApiController
    {
        [HttpPut]
        [Route("api/v1/File/UpdateCustomerProfile")]

        public async Task<IActionResult> Put([FromForm(Name = "image")] IFormFile formFile)
        {
            var randomFile = Path.GetRandomFileName();


            if (formFile.Length > 0)
            {
                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }
            }


            Response<bool> updateProfilePicture = new Response<bool>();
            return this.CreatePutHttpResponse(updateProfilePicture);

        }
    }
}
