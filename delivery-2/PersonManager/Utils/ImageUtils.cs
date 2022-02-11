using System.Data.SqlClient;
using System.IO;
using System.Windows.Media.Imaging;

namespace Zadatak.Utils
{
    public static class ImageUtils
    {
        public static BitmapImage ByteArrayToBitmapImage(byte[] picture)
        {
            using (var memoryStream = new MemoryStream(picture))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = memoryStream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
        }

        public static byte[] BitmapImageToByteArray(BitmapImage image)
        {
            var jpegEncoder = new JpegBitmapEncoder();
            jpegEncoder.Frames.Add(BitmapFrame.Create(image));
            using (var memoryStream = new MemoryStream())
            {
                jpegEncoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static byte[] ByteArrayFromSqlDataReader(SqlDataReader dr, int column)
        {
            int bufferSize = 1024;
            int currentBytes = 0;
            byte[] buffer = new byte[bufferSize];
            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    int readBytes;
                    do
                    {
                        readBytes = (int)dr.GetBytes(column, currentBytes, buffer, 0, bufferSize);
                        binaryWriter.Write(buffer, 0, readBytes);
                        currentBytes += readBytes;
                    } while (readBytes == bufferSize);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
