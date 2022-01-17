using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.Linq;
using System;

namespace S3Repository {
    public interface IBucketRepository {
        Task<IList<S3Object>> GetObjectsInBucketAsync(CancellationToken cancellationToken);
        Task<IList<S3Object>> GetObjectsInPathAsync(string path, CancellationToken cancellationToken);
        Task CreateFolderAsync(string remotePath);
        Task CreateFoldersAsync(IList<string> remotePaths);
        Task UploadFileAsync(string remoteFilename, string localFilename, Dictionary<string, string>? metadata = null);
        Task DownloadFileAsync(string remoteFilename, string localSavePath);
        Task<string> DownloadFileContentsAsync(string remoteFilename, string versionID = "");
        Task<byte[]> DownloadFileByteContentsAsync(string remoteFilename);
        Task CopyFileAsync(string sourceKey, string destinationKey);
        Task DeleteFolderAsync(string remotePath);
        Task DeleteFileAsync(string remotePath);
        Task DeleteFileVersionAsync(string remotePath, string versionID);
    }
}