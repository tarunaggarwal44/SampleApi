using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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
        //[HttpPut]
        //[Route("api/v1/File/UpdateCustomerProfile")]

        //public async Task<IActionResult> Put()
        //{




        //    //string path = CreateTempFile(SampleFileContent);

        //    //string connectionString = "DefaultEndpointsProtocol=https;AccountName=tarun4;AccountKey=53+6zh++k4ufBhah2yRBT7JQrBEvvnhEhBp7HgFKHAeBrIw+cLBL0BFN0u7Tlp5/I1LS7i4yvHkY+mwOtShBhw==;EndpointSuffix=core.windows.net";

        //    string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1";

        //    // Get a reference to a container named "sample-container" and then create it
        //    BlobContainerClient container = new BlobContainerClient(connectionString, "image");
        //    //await container.CreateAsync();
        //    try
        //    {
        //        // Get a reference to a blob
        //        BlobClient blob = container.GetBlobClient("new");

        //        // Upload file dat
        //        var path = @"C:\Users\taruna\Desktop\temp.jpg";
        //        await blob.UploadAsync(path);

        //        // Verify we uploaded some content
        //        //BlobProperties properties = await blob.GetPropertiesAsync();
        //        //Assert.AreEqual(SampleFileContent.Length, properties.ContentLength);
        //    }
        //    finally
        //    {
        //        // Clean up after the test when we're finished
        //        //await container.DeleteAsync();
        //    }

        //    Response<bool> updateProfilePicture = new Response<bool>();
        //    return this.CreatePutHttpResponse(updateProfilePicture);

        //}


        [HttpPut]
        [Route("api/v1/File/UpdateCustomerProfile")]

        public async Task<IActionResult> Put([FromForm(Name = "image")] IFormFile formFile)
        {

            if (formFile.Length > 0)
            {
                //var filePath = Path.GetTempFileName();

                string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1";
                var containterName = "image";
                BlobContainerClient container = new BlobContainerClient(connectionString, containterName);

                BlobClient blob = container.GetBlobClient(formFile.FileName);

                BlobHttpHeaders blobHttpHeaders = new BlobHttpHeaders();
                blobHttpHeaders.ContentType = "image/png";
                using (var stream = formFile.OpenReadStream())
                {
                    await blob.UploadAsync(stream, blobHttpHeaders);
                }
            }






            Response<bool> updateProfilePicture = new Response<bool>();
            return this.CreatePutHttpResponse(updateProfilePicture);

        }
    }
}
