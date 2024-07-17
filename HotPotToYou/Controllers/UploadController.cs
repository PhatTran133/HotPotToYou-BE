using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
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
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName = "image-hotpottoyou";
        public UploadController()
        {
            _s3Client = new AmazonS3Client();
        }

        [HttpPost("upload")]
        public async Task<ActionResult<JsonResponse<string>>> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = file.FileName,
                    FilePath = filePath,
                    ContentType = file.ContentType
                };

                await _s3Client.PutObjectAsync(putRequest);
                string fileUrl = $"https://{_bucketName}.s3.amazonaws.com/{file.FileName}";

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

        [HttpDelete("upload")]
        public async Task DeleteFile(string imageUrl)
        {

            Uri uri;
            if (!Uri.TryCreate(imageUrl, UriKind.Absolute, out uri))
            {
                throw new ArgumentException("Invalid image URL");
            }

            var imageKey = Path.GetFileName(uri.LocalPath);

            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = imageKey
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);

        }

    }
}


