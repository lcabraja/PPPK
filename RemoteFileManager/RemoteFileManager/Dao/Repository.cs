using S3Repository;
using System;

namespace RemoteFileManager.Dao {
    static class Repository {
        private static readonly Lazy<AWSBucketRepository> bucket = new(() => {
            var credentials = Doppler.FetchSecrets();
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
