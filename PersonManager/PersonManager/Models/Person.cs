using System.Windows.Media.Imaging;
using Zadatak.Utils;

namespace Zadatak.Models
{
    public class Person 
    {
        public int IDPerson { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age{ get; set; }
        public string Email { get; set; }
        public byte[] Picture { get; set; }
        public BitmapImage Image
        {
            get => ImageUtils.ByteArrayToBitmapImage(Picture);
        }

    }
}
