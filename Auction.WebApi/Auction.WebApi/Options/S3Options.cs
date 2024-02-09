namespace Auction.WebApi.Options
{
    public class S3Options
    {
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string BucketName { get; set; } = string.Empty;
        public string BucketUrl { get; set; } = string.Empty;
        public string FolderName { get; set; } = string.Empty;
        public string ServiceUrl {  get; set; } = string.Empty;
    }
}
