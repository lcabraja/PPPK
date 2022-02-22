using System.Windows.Controls;
using Zadatak.ViewModels;

namespace Zadatak
{

    public class FramedPage : Page
    {
        public FramedPage(PersonViewModel personViewModel)
        {
            PersonViewModel = personViewModel;
        }
        public FramedPage(PersonViewModel personViewModel, PetViewModel petViewModel) {
            PersonViewModel = personViewModel;
            PetViewModel = petViewModel;
        }

        public PersonViewModel PersonViewModel { get; }
        public PetViewModel PetViewModel { get; }
        public Frame Frame { get; set; }
    }
}
