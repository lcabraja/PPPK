using Azure.Storage.Blobs;
using S3Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteFileManager.Dao {
    static class Repository {
        private const string ContainerName = "blobcontainer";

        //private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        //private static readonly Lazy<BlobContainerClient> container = new(() => new BlobServiceClient(cs).GetBlobContainerClient(ContainerName));

        private static readonly Lazy<AWSBucketRepository> bucket = new(() => new AWSBucketRepository(
            "",
            "",
            ""
            ));
        //public static BlobContainerClient Container => container.Value;
        public static AWSBucketRepository Bucket => bucket.Value;
    }
}
