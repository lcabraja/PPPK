using PersonManager.Models;
using PersonManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonManager
{
    /// <summary>
    /// Interaction logic for EditPersonPage.xaml
    /// </summary>
    public partial class EditPersonPage : FramedPage
    {
        private readonly Person person;
        public EditPersonPage(PersonViewModel personViewModel, Person person = null) : base(personViewModel)
        {
            InitializeComponent();
            this.person = person ?? new Person();
            DataContext = person;
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();
        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
