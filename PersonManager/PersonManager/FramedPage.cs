using PersonManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PersonManager
{
    public class FramedPage : Page
    {

        public PersonViewModel PersonViewModel { get; }
        public Frame Frame { get; set; }

        public FramedPage(PersonViewModel personViewModel)
        {
            PersonViewModel = personViewModel;
        }
    }
}
