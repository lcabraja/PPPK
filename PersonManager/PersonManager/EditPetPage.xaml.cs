using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zadatak.Models;
using Zadatak.Utils;
using Zadatak.ViewModels;

namespace Zadatak {
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPetPage : FramedPage {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
        private readonly Pet pet;
        public EditPetPage(PersonViewModel personViewModel, PetViewModel petViewModel, Pet pet = null) : base(personViewModel, petViewModel) {
            InitializeComponent();
            this.pet = pet ?? new Pet();
            DataContext = pet;

            //var binding = new Binding("Test") {
            //    Source = PersonViewModel.People,
            //    Converter = new Converters.PetToOwnerName()
            //};
            //BindingOperations.SetBinding(CbOwners, ComboBox.ItemsSourceProperty, binding);
            CbOwners.ItemsSource = PersonViewModel.People;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e) {
            if (FormValid()) {
                pet.Age = int.Parse(TbAge.Text.Trim());
                pet.Name = TbName.Text.Trim();
                pet.OwnerID = (CbOwners.SelectedItem as Person).IDPerson;
                pet.Picture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);
                if (pet.IDPet == 0) {
                    PetViewModel.Pets.Add(pet);
                } else {
                    PetViewModel.Update(pet);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private bool FormValid() {
            bool valid = true;
            GridContainter.Children.OfType<TextBox>().ToList().ForEach(e => {
                if (string.IsNullOrEmpty(e.Text.Trim())
                    || ("Int".Equals(e.Tag) && !int.TryParse(e.Text, out int age))) {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                } else {
                    e.Background = Brushes.White;
                }
            });
            if (Picture.Source == null) {
                PictureBorder.BorderBrush = Brushes.LightCoral;
                valid = false;
            } else {
                PictureBorder.BorderBrush = Brushes.WhiteSmoke;
            }
            return valid;
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog() {
                Filter = Filter
            };
            if (openFileDialog.ShowDialog() == true) {
                Picture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
