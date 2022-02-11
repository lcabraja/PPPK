using S3Repository;
using System;

namespace RemoteFileManager.Dao {
    static class Repository {
        public static string BucketName { get; private set; }
        public static string PublicURL { get => $"https://s3.eu-central-1.amazonaws.com/{BucketName}/"; }

        private static readonly Lazy<AWSBucketRepository> bucket = new(() => {
            var credentials = Doppler.FetchSecrets();
            BucketName = credentials.BucketName;
            return new AWSBucketRepository(
                credentials.BucketName,
                credentials.AccessKey,
                credentials.Secret
            );
        });
        //public static BlobContainerClient Container => container.Value;
        public static AWSBucketRepository Bucket => bucket.Value;
    }
}
