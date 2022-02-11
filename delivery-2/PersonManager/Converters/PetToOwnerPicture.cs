using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Zadatak.Dal;
using Zadatak.Models;

namespace Zadatak.Converters {
    public class PetToOwnerPicture : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is Pet pet) {
                return RepositoryFactory.GetRepository().GetPerson(pet.OwnerID).Picture;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
