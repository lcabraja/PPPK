using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Repository {
    public class AWSBucketRepository : IBucketRepository {

        public string BucketName { get; set; }

        public event AmazonFileProgressChangeEventHandler? FileProgressChanged;
        private readonly S3StorageClass _storageClass = S3StorageClass.Standard;
        private AmazonS3Client _client;
        private TransferUtility _transfer;

        public AWSBucketRepository(string bucketName, Amazon.Runtime.AWSCredentials credentials) {
            BucketName = bucketName;
            _client = new AmazonS3Client(credentials, RegionEndpoint.EUCentral1);
            _transfer = new TransferUtility(_client);
        }

        public AWSBucketRepository(string bucketName, Amazon.Runtime.AWSCredentials credentials, AmazonS3Config config) {
            BucketName = bucketName;
            _client = new AmazonS3Client(credentials, config);
            _transfer = new TransferUtility(_client);
        }

        public AWSBucketRepository(string bucketName, string accessKeyID, string secret) {
            BucketName = bucketName;
            _client = new AmazonS3Client(accessKeyID, secret, RegionEndpoint.EUCentral1);
            _transfer = new TransferUtility(_client);
        }

        public AWSBucketRepository(string bucketName, string accessKeyID, string secret, string endpoint) {
            BucketName = bucketName;
            _client = new AmazonS3Client(accessKeyID, secret, new AmazonS3Config { ServiceURL = endpoint });
            _transfer = new TransferUtility(_client);
        }

        public async Task<IList<string>> GetBucketsAsync() {
            ListBucketsResponse resp = await _client.ListBucketsAsync();
            IList<string> buckets = new List<string>();
            foreach (S3Bucket bucket in resp.Buckets)
                buckets.Add(bucket.BucketName);
            return buckets;
        }
        public async Task<IList<S3Object>> GetObjectsInBucketAsync(CancellationToken cancellationToken) {
            List<S3Object> items = new();
            ListObjectsRequest? req = new() {
                BucketName = BucketName,
            };

            do {
                ListObjectsResponse resp = await _transfer.S3Client.ListObjectsAsync(req, cancellationToken);
                if (resp.IsTruncated && !cancellationToken.IsCancellationRequested)
                    req.Marker = resp.NextMarker;
                else
                    req = null;
                items.AddRange(resp.S3Objects);
            }
            while (req != null);
            return items;
        }
        public async Task<IList<S3Object>> GetObjectsInPathAsync(string path, CancellationToken cancellationToken) {
            List<S3Object> items = new();
            ListObjectsRequest? req = new() {
                BucketName = BucketName,
                Prefix = path
            };

            do {
                ListObjectsResponse resp = await _transfer.S3Client.ListObjectsAsync(req, cancellationToken);
                if (resp.IsTruncated && !cancellationToken.IsCancellationRequested)
                    req.Marker = resp.NextMarker;
                else
                    req = null;
                items.AddRange(resp.S3Objects);
            }
            while (req != null);
            return items;
        }
        public async Task CreateFolderAsync(string remotePath) {
            if (remotePath.Last() != '/') remotePath += "/";
            PutObjectRequest req = new() {
                BucketName = BucketName,
                Key = remotePath,
                ContentBody = string.Empty
            };
            _ = await _transfer.S3Client.PutObjectAsync(req);
        }
        public async Task CreateFoldersAsync(IList<string> remotePaths) {
            _ = await Task
                .WhenAll(remotePaths
                    .Select(path => path.Last() != '/' ? path + "/" : path)
                    .Select(path => new PutObjectRequest() {
                        BucketName = BucketName,
                        Key = path,
                        ContentBody = string.Empty
                    })
                    .Select(async req => await _transfer.S3Client.PutObjectAsync(req)));
        }
        public async Task UploadFileAsync(string remoteFilename, string localFilename, Dictionary<string, string>? metadata = null) {
            if (remoteFilename[0] == '/') remoteFilename = remoteFilename[1..];
            TransferUtilityUploadRequest uploadRequest = new() {
                BucketName = BucketName,
                FilePath = localFilename,
                Key = remoteFilename,
                AutoCloseStream = true
            };
            if (metadata != null)
                foreach (var kvp in metadata)
                    uploadRequest.Metadata.Add(kvp.Key, kvp.Value);
            uploadRequest.UploadProgressEvent += (sender, e) =>
                FileProgressChanged?.Invoke(
                    e.FilePath,
                    e.TransferredBytes,
                    e.TotalBytes,
                    e.PercentDone);
            await _transfer.UploadAsync(uploadRequest);
        }
        public async Task DownloadFileAsync(string remoteFilename, string localSavePath) {
            TransferUtilityDownloadRequest req = new() {
                BucketName = BucketName,
                FilePath = localSavePath,
                Key = remoteFilename
            };
            req.WriteObjectProgressEvent += (s, e) =>
                FileProgressChanged?.Invoke(
                    e.Key,
                    e.TransferredBytes,
                    e.TotalBytes,
                    e.PercentDone);
            await _transfer.DownloadAsync(req);
        }
        public async Task<string> DownloadFileContentsAsync(string remoteFilename, string versionID = "") {
            string tempFile = Path.GetTempFileName();
            TransferUtilityDownloadRequest req = new() {
                BucketName = BucketName,
                Key = remoteFilename,
                VersionId = !string.IsNullOrEmpty(versionID) ? versionID : null,
                FilePath = tempFile
            };
            req.WriteObjectProgressEvent += (s, e) =>
                FileProgressChanged?.Invoke(
                    e.Key,
                    e.TransferredBytes,
                    e.TotalBytes,
                    e.PercentDone);
            await _transfer.DownloadAsync(req);
            string fileContents = await File.ReadAllTextAsync(tempFile);
            File.Delete(tempFile);
            return fileContents;
        }
        public async Task<byte[]> DownloadFileByteContentsAsync(string remoteFilename) {
            byte[] buffer;
            TransferUtilityOpenStreamRequest req = new() {
                BucketName = BucketName,
                Key = remoteFilename
            };
            using (Stream s = await _transfer.OpenStreamAsync(req)) {
                buffer = new byte[s.Length];
                s.Position = 0;
                _ = await s.ReadAsync(buffer.AsMemory(0, (int) s.Length));
                await s.FlushAsync();
            }
            return buffer;
        }

        public async Task CopyFileAsync(string sourceKey, string destinationKey) {
            CopyObjectRequest request = new() {
                SourceBucket = BucketName,
                SourceKey = sourceKey,
                DestinationBucket = BucketName,
                DestinationKey = destinationKey,
                StorageClass = _storageClass
            };
            _ = await _transfer.S3Client.CopyObjectAsync(request);
        }
        public async Task DeleteFolderAsync(string remotePath) {
            if (remotePath[^1] != '/') remotePath += "/";
            ListObjectsRequest? req = new() {
                BucketName = BucketName,
                Prefix = remotePath,
                Delimiter = "/"
            };
            do {
                ListObjectsResponse resp = await _transfer.S3Client.ListObjectsAsync(req);
                if (resp.IsTruncated)
                    req.Marker = resp.NextMarker;
                else
                    req = null;
                DeleteObjectsRequest deleteRequest = new() {
                    BucketName = BucketName
                };
                if (resp.S3Objects.Count == 0) break;
                foreach (var item in resp.S3Objects)
                    deleteRequest.AddKey(item.Key);
                _ = await _transfer.S3Client.DeleteObjectsAsync(deleteRequest);
            }
            while (req != null);
        }
        public async Task DeleteFileAsync(string remotePath) {
            DeleteObjectRequest req = new() {
                BucketName = BucketName,
                Key = remotePath
            };
            _ = await _transfer.S3Client.DeleteObjectAsync(req);
        }
        public async Task DeleteFileVersionAsync(string remotePath, string versionID) {
            DeleteObjectRequest req = new() {
                BucketName = BucketName,
                Key = remotePath,
                VersionId = versionID
            };
            _ = await _transfer.S3Client.DeleteObjectAsync(req);
        }
    }
}

