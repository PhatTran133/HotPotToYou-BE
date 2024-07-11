using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HotPotToYou.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private const string bucketName = "image-hotpottoyou";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSoutheast2;
        private readonly IAmazonS3 s3Client;
        public UploadController()
        {
            var awsAccessKeyId = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var awsSecretAccessKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");

            s3Client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, bucketRegion);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var keyName = file.FileName;

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    var fileTransferUtility = new TransferUtility(s3Client);
                    await fileTransferUtility.UploadAsync(stream, bucketName, keyName);
                }

                var fileUrl = $"https://{bucketName}.s3.{bucketRegion.SystemName}.amazonaws.com/{keyName}";
                return Ok(new JsonResponse<string>(fileUrl));
            }
            catch (AmazonS3Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Error encountered on server. Message:'{e.Message}'" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Unknown encountered on server. Message:'{e.Message}'" });
            }
        }
    }
}


