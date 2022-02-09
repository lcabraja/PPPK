using System.Windows;
using System.Windows.Controls;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak {
    /// <summary>
    /// Interaction logic for ListPersonsPage.xaml
    /// </summary>
    public partial class ListPage : FramedPage {
        //public ListPeoplePage(PersonViewModel personViewModel) : base(personViewModel) {
        //    InitializeComponent();
        //    LvUsers.ItemsSource = personViewModel.People;
        //}

        public ListPage(PersonViewModel personViewModel, PetViewModel petViewModel) : base(personViewModel, petViewModel) {
            InitializeComponent();
            LvUsers.ItemsSource = personViewModel.People;
            LvPets.ItemsSource = petViewModel.Pets;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(new EditPersonPage(PersonViewModel)
                { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e) {
            if (LvUsers.SelectedItem != null) {
                Frame.Navigate(new EditPersonPage(PersonViewModel, LvUsers.SelectedItem as Person)
                    { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            if (LvUsers.SelectedItem != null) {
                PersonViewModel.People.Remove(LvUsers.SelectedItem as Person);
            }
        }

        private void BtnAddPet_Click(object sender, RoutedEventArgs e)
            => Frame.Navigate(new EditPetPage(PersonViewModel, PetViewModel)
                { Frame = Frame });

        private void BtnEditPet_Click(object sender, RoutedEventArgs e) {
            if (LvUsers.SelectedItem != null) {
                Frame.Navigate(new EditPetPage(PersonViewModel, PetViewModel, LvPets.SelectedItem as Pet)
                    { Frame = Frame });
            }
        }

        private void BtnDeletePet_Click(object sender, RoutedEventArgs e) {
            if (LvUsers.SelectedItem != null) {
                PetViewModel.Pets.Remove(LvPets.SelectedItem as Pet);
            }
        }
    }
}
