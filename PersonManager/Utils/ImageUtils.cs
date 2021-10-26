using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PersonManager.Utils
{
    public class ImageUtils
    {
        public static BitmapImage ByteArrayToBitmapImage(byte[] picture)
        {
            using (var memoryStream = new MemoryStream(picture))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }

        public static byte[] BitmapImageToByteArray(BitmapImage image)
        {
            var jpegBitmapEncoder = new JpegBitmapEncoder();
            jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(image));
            using (var memoryStream = new MemoryStream())
            {
                jpegBitmapEncoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static byte[] ByteArrayFromSqlDataReader(SqlDataReader sqlDataReader, int column)
        {
            int bufferSize = 1024;
            int currentBytes = 0;
            byte[] buffer = new byte[bufferSize];
            using (var memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(memoryStream))
                {
                    int readBytes;
                    do
                    {
                        readBytes = (int)sqlDataReader.GetBytes(column, currentBytes, buffer, 0, bufferSize);
                        writer.Write(buffer, 0, readBytes);
                        currentBytes += readBytes;
                    } while (readBytes == bufferSize);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
