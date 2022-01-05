using Azure.Storage.Blobs.Models;
using RemoteFileManager.Dao;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteFileManager.ViewModels
{
    class ItemsViewModel
    {
        public const string ForwardSlash = "/";
        private string directory;

        public string Directory
        {
            get => directory;
            set
            {
                directory = value;
                Refresh();
            }
        }

        public ObservableCollection<BlobItem> Items { get; }
        public ObservableCollection<string> Directories { get; }

        public ItemsViewModel()
        {
            Items = new ObservableCollection<BlobItem>();
            Directories = new ObservableCollection<string>();
            Refresh();
        }

        private void Refresh()
        {
            Items.Clear();
            Directories.Clear();
            Repository.Container.GetBlobs().ToList().ForEach(item =>
            {
                if (item.Name.Contains(ForwardSlash))
                {
                    string dir = item.Name.Substring(0, item.Name.LastIndexOf(ForwardSlash));
                    if (!Directories.Contains(dir))
                    {
                        Directories.Add(dir);
                    }
                }

                // handle root
                if (string.IsNullOrEmpty(Directory) && !item.Name.Contains(ForwardSlash))
                {
                    Items.Add(item);
                }
                else if (!string.IsNullOrEmpty(Directory) && item.Name.Contains(Directory + ForwardSlash))
                {
                    Items.Add(item);
                }
            });
        }

        public async Task DeleteAsync(BlobItem blobItem)
        {
            await Repository.Container.GetBlobClient(blobItem.Name).DeleteAsync();
            Refresh();
        }
        public async Task DownloadAsync(BlobItem blobItem, string filename)
        {
            using (var fs = File.OpenWrite(filename))
            {
                await Repository.Container.GetBlobClient(blobItem.Name).DownloadToAsync(fs);
            }
        }
        public async Task UploadAsync(string path, string dir)
        {
            string filename = Path.GetFileName(path);
            if (!string.IsNullOrEmpty(dir))
            {
                filename = dir + ItemsViewModel.ForwardSlash + filename;
            }
            using (var fs = File.OpenRead(path))
            {
                await Repository.Container.GetBlobClient(filename).UploadAsync(fs, true);
            }
            Refresh();
        }
    }
}