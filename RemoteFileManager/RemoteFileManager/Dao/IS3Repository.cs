using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace S3Repository {
	public interface IS3Repository {
		Task<IList<string>> GetBucketsAsync();
		Task<IList<S3Object>> GetObjectsInBucketAsync(string bucketName, string path, CancellationToken cancellationToken);
		Task<IList<S3Object>> GetObjectsInPathAsync(string bucketName, string path, CancellationToken cancellationToken);
		Task CreateFolderAsync(string bucketName, string remotePath);
		Task CreateFoldersAsync(string bucketName, IList<string> remotePaths);
		Task UploadFileAsync(string bucketName, string remoteFilename, string localFilename, Dictionary<string, string>? metadata = null);
		Task DownloadFileAsync(string bucketName, string remoteFilename, string localSavePath);
		Task<string> DownloadFileContentsAsync(string bucketName, string remoteFilename, string versionID = "");
		Task<byte[]> DownloadFileByteContentsAsync(string bucketName, string remoteFilename);
		Task CopyFileAsync(string sourceBucketName, string sourceKey, string destinationKey);
		Task CopyFileAsync(string sourceBucketName, string destinationBucketName, string sourceKey, string destinationKey);
		Task DeleteFolderAsync(string bucketName, string remotePath);
		Task DeleteFileAsync(string bucketName, string remotePath);
		Task DeleteFileVersionAsync(string bucketName, string remotePath, string versionID);
	}
}