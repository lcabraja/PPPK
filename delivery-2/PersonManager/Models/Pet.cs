using System.Windows.Media.Imaging;
using Zadatak.Utils;

namespace Zadatak.Models
{
    public class Pet 
    {
        public int IDPet { get; set; }
        public string Name { get; set; }
        public int Age{ get; set; }
        public int OwnerID { get; set; }
        public byte[] Picture { get; set; }
        public BitmapImage Image
        {
            get => ImageUtils.ByteArrayToBitmapImage(Picture);
        }

    }
}
