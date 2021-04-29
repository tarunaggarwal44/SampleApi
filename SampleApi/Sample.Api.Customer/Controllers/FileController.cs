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
            var a = HttpContext.Request.ContentType;

            string[] permittedExtensions = { ".jpeg", ".jpg", ".png", ".gif" };

            var ext = Path.GetExtension(formFile.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                // The extension is invalid ... discontinue processing the file
            }

            var maxFileSize = 3145728;
            if (formFile.Length > 0 && formFile.Length < maxFileSize)
            {
                //var filePath = Path.GetTempFileName();

                string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1";
                var containterName = "customer-profile-picture";
                var fileName = "file";
                BlobContainerClient container = new BlobContainerClient(connectionString, containterName);

                BlobClient blob = container.GetBlobClient(fileName);



                BlobHttpHeaders blobHttpHeaders = new BlobHttpHeaders();
                blobHttpHeaders.ContentType = "image/jpeg";
                //var isMatch = MatchExtension(stream, ext);

                using (var stream = formFile.OpenReadStream())
                {
                    var rr2 = blob.Uri.AbsoluteUri;
                    await blob.UploadAsync(stream, blobHttpHeaders);
                    var rr = blob.Uri.AbsoluteUri;
                }
            }






            Response<bool> updateProfilePicture = new Response<bool>();
            return this.CreatePutHttpResponse(updateProfilePicture);

        }


        private bool MatchExtension(IFormFile formFile, string ext)
        {
            Dictionary<string, List<byte[]>> _fileSignature =
   new Dictionary<string, List<byte[]>>
{
    { ".jpeg", new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
        }
    },
    { ".jpg", new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
        }
    },
      { ".gif", new List<byte[]>
        {
            new byte[] { 0x47, 0x49, 0x46, 0x38 },
        }
    },

      { ".png", new List<byte[]>
        {
            new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A },
        }
    },
};

            using (var stream = formFile.OpenReadStream())
            {
                using (var reader = new BinaryReader(stream))
                {
                    var signatures = _fileSignature[ext];
                    var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                    return signatures.Any(signature =>
                        headerBytes.Take(signature.Length).SequenceEqual(signature));
                }
            }


        }



    }
}
