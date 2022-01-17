using Amazon.S3.Model;
using Azure.Storage.Blobs.Models;
using RemoteFileManager.Dao;
using RemoteFileManager.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteFileManager.ViewModels {
    class ItemsViewModel {
        public const string ForwardSlash = "/";
        private string directory = "";
        private System.Threading.CancellationToken? cancelRefresh;

        public string Directory {
            get => directory;
            set {
                directory = value;
                Refresh();
            }
        }

        public ObservableCollection<string> Items { get; }
        public ObservableCollection<string> Directories { get; }

        public ItemsViewModel() {
            Items = new();
            Directories = new ObservableCollection<string>();
            Refresh();
        }

        private async void Refresh() {
            if (cancelRefresh == null || !cancelRefresh.Value.IsCancellationRequested) {
                cancelRefresh = new System.Threading.CancellationToken();

                Items.Clear();
                Directories.Clear();
                Directories.Add("");
                (await Repository.Bucket.GetObjectsInBucketAsync(cancelRefresh.GetValueOrDefault()))
                    .GetDirectories()
                    .ToList()
                    .ForEach(dir => Directories.Add(dir));
                (await Repository.Bucket.GetObjectsInPathAsync(Directory, cancelRefresh.GetValueOrDefault()))
                    .GetDirectoryContents(Directory)
                    .ToList()
                    .ForEach(item => Items.Add(item.GetName(Directory)));
            }
            cancelRefresh = null;
        }

        public async Task DeleteAsync(S3Object blobItem) {
            await Repository.Bucket.DeleteFileAsync(blobItem.Key);
            Refresh();
        }
        public async Task DownloadAsync(S3Object blobItem, string filename) {
            await Repository.Bucket.DownloadFileAsync(blobItem.Key, filename);
        }
        public async Task UploadAsync(string path, string dir) {
            string filename = Path.GetFileName(path);
            if (!string.IsNullOrEmpty(dir)) {
                filename = dir + filename;
            }
            await Repository.Bucket.UploadFileAsync(filename, path);
            Refresh();
        }
    }
}