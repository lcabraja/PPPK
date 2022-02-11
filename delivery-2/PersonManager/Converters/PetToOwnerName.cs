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
    public class PetToOwnerName : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is Pet pet) {
                var owner = RepositoryFactory.GetRepository().GetPerson(pet.OwnerID);
                return $"{owner.FirstName} {owner.LastName}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
