using Amazon.S3.Model;
using Microsoft.Win32;
using RemoteFileManager.Converters;
using RemoteFileManager.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace RemoteFileManager {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly ItemsViewModel itemsViewModel;
        public string Directory { get => itemsViewModel.Directory; }
        public MainWindow() {
            InitializeComponent();
            itemsViewModel = new ItemsViewModel();
            Init();
        }

        private void Init() {
            CbDirectories.ItemsSource = itemsViewModel.Directories;
            LbItems.ItemsSource = itemsViewModel.Items;

            //S3ObjectToString s3StringFormatter = new();
            //Binding binding = new Binding("itemsViewMode.Items");
            //binding.Converter = s3StringFormatter;
            //LbItems.DataContext = itemsViewModel.Items;
        }

        private void CbDirectories_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                itemsViewModel.Directory = CbDirectories.Text;
            }
        }

        private void CbDirectories_TextChanged(object sender, TextChangedEventArgs e) {
            if (itemsViewModel.Directories.Contains(CbDirectories.Text)) {
                itemsViewModel.Directory = CbDirectories.Text;
                CbDirectories.SelectedItem = itemsViewModel.Directory;
            }
        }

        private void LbItems_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (LbItems.SelectedItem != null && LbItems.SelectedItem is S3Object s3Object) {
                DataContext = s3Object;
            } else {
                DataContext = null;
            }
        }

        private async void BtnUpload_Click(object sender, RoutedEventArgs e) {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {
                await itemsViewModel.UploadAsync(openFileDialog.FileName, CbDirectories.Text);
            }
            CbDirectories.Text = itemsViewModel.Directory;
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e) {
            if (LbItems.SelectedItem is not S3Object s3object) {
                return;
            }
            var saveFileDialog = new SaveFileDialog {
                FileName = System.IO.Path.GetFileName(s3object.Key)
            };
            if (saveFileDialog.ShowDialog() == true) {
                await itemsViewModel.DownloadAsync(s3object, saveFileDialog.FileName);
            }
            CbDirectories.Text = itemsViewModel.Directory;
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e) {
            if (LbItems.SelectedItem is not S3Object s3object) {
                return;
            }
            await itemsViewModel.DeleteAsync(s3object);
            CbDirectories.Text = itemsViewModel.Directory;
        }
    }
}
