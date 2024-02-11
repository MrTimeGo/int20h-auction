using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Auction.WebApi.Options;
using Auction.WebApi.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Sockets;

namespace Auction.WebApi.Services.Implementations
{
    public class UploadFileService(IConfiguration configuration, IAmazonS3 s3) : IUploadFileService
    {
        private S3Options? options = configuration
                .GetSection("AWS")
                .Get<S3Options>();
        public async Task<string[]> UploadListFilesAsync(List<IFormFile> files)
        {
            if (!await CheckIfBucketExistAsync())
            {
                await CreateBucketAsync();
            }
            var tasks = files.Select(UploadFileAsync).ToArray();
            return await Task.WhenAll(tasks);
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (!await CheckIfBucketExistAsync())
            {
                await CreateBucketAsync();
            }
            var objectKey = Path.Join(options!.FolderName, Guid.NewGuid().ToString() + "." + file.FileName.Split('.', StringSplitOptions.RemoveEmptyEntries).Last());

            await using var stream = file.OpenReadStream();
            await s3.PutObjectAsync(new()
            {
                BucketName = options.BucketName,
                InputStream = stream,
                Key = objectKey,
                ContentType = file.ContentType,
                CannedACL = S3CannedACL.PublicRead
            });

            stream.Close();

            return options.BucketUrl + '/' + objectKey;
        }

        private async Task<bool> CheckIfBucketExistAsync()
        {
            if (string.IsNullOrEmpty(options!.BucketName)) return false;
            return await AmazonS3Util.DoesS3BucketExistV2Async(s3, options!.BucketName);
            
        }

        private async Task CreateBucketAsync()
        {
            await s3.PutBucketAsync(new PutBucketRequest { BucketName = options!.BucketName, UseClientRegion = true });
        }
    }
}
