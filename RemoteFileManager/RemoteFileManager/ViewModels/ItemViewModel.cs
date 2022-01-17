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
                var temp = value;
                if (!temp.EndsWith(ForwardSlash)) {
                    temp += ForwardSlash;
                }
                directory = temp;
                Refresh();
            }
        }

        public ObservableCollection<S3Object> Items { get; }
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
                (await Repository.Bucket.GetObjectsInBucketAsync(cancelRefresh.GetValueOrDefault())).ToList().ForEach(item => {
                    if (item.Key.Contains(ForwardSlash)) {
                        string directory = item.Key.Substring(0, item.Key.LastIndexOf(ForwardSlash));
                        if (!Directories.Contains(directory)) {
                            Directories.Add(directory);
                        }
                    }
                    if (string.IsNullOrEmpty(Directory) && !item.Key.Contains(ForwardSlash)) {
                        Items.Add(item);
                    } else if (!string.IsNullOrEmpty(Directory) && item.Key.Contains($"{Directory}{ForwardSlash}")) {
                        Items.Add(item);
                    }
                });
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
                filename = dir + ForwardSlash + filename;
            }
            await Repository.Bucket.UploadFileAsync(path, filename);
            Refresh();
        }
    }
}