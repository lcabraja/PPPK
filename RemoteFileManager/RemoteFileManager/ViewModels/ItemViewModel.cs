using Amazon.S3.Model;
using RemoteFileManager.Dao;
using RemoteFileManager.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteFileManager.ViewModels {
    class ItemsViewModel {
        public const string ForwardSlash = "/";
        private string directory = "";
        private System.Threading.CancellationTokenSource cancelRefresh = new();

        public string Directory {
            get => directory;
            set {
                directory = value;
                Refresh();
            }
        }

        public ObservableCollection<S3Object> Items { get; }
        public ObservableCollection<string> Directories { get; }

        public ItemsViewModel() {
            Items = new();
            Directories = new();
            Refresh();
        }

        private bool isRefreshing = false;
        private async void Refresh() {
            if (!isRefreshing) {
                isRefreshing = true;
                try {
                    if (!cancelRefresh.IsCancellationRequested) {
                        Directories.Clear();
                        Directories.Add("");
                        (await Repository.Bucket.GetObjectsInBucketAsync(cancelRefresh.Token))
                        .GetDirectories()
                        .ToList()
                        .ForEach(dir => Directories.Add(dir));
                    }
                    if (!cancelRefresh.IsCancellationRequested) {
                        Items.Clear();
                        (await Repository.Bucket.GetObjectsInPathAsync(Directory, cancelRefresh.Token))
                        .GetDirectoryContents(Directory)
                        .ToList()
                        .ForEach(item => Items.Add(item));
                    }
                } catch (TaskCanceledException) {
                } finally {
                    cancelRefresh.Dispose();
                    cancelRefresh = new();
                    isRefreshing = false;
                }
            } else {
                cancelRefresh.Cancel();
                cancelRefresh.Dispose();
                cancelRefresh = new();
                isRefreshing = false;
                Refresh();
            }
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