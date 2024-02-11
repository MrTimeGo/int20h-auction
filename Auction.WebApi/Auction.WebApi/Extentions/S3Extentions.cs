using Amazon.Runtime;
using Amazon.S3;
using Amazon;
using Auction.WebApi.Options;

namespace Auction.WebApi.Extentions
{
    public static class S3Extentions
    {
        public static IServiceCollection AddDevAws(this IServiceCollection services, IConfiguration configuration)
        {
            var awsOptions = GetAwsOptions(configuration);

            var s3Client = new AmazonS3Client(
                new BasicAWSCredentials(awsOptions.AccessKey, awsOptions.SecretKey), 
                new AmazonS3Config()
                {
                    ServiceURL = awsOptions.ServiceUrl,
                    //ForcePathStyle = true,
                }
            );

            services.AddSingleton<IAmazonS3>(s3Client);
            return services;
        }

        public static IServiceCollection AddProdAws(this IServiceCollection services, IConfiguration configuration)
        {
            var awsOptions = GetAwsOptions(configuration);

            services.AddDefaultAWSOptions(new()
            {
                Region = RegionEndpoint.GetBySystemName(awsOptions.Region),
                Credentials = new BasicAWSCredentials(awsOptions.AccessKey, awsOptions.SecretKey),
            });

            services.AddAWSService<IAmazonS3>();

            return services;
        }
        private static S3Options GetAwsOptions(IConfiguration configuration)
        {
            var awsOptions = configuration
                .GetSection("AWS")
                .Get<S3Options>();

            if (awsOptions == null)
            {
                throw new ArgumentNullException(nameof(awsOptions));
            }

            return awsOptions;
        }
    }
    
}
