using Amazon.S3.Model;
using RemoteFileManager.Dao;
using RemoteFileManager.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RemoteFileManager.Converters {
    public class S3ObjectToS3URLConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is S3Object s3Object) {
                return $"{Repository.PublicURL}{s3Object.Key}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string url) {
                return url[Repository.PublicURL.Length..];
            }
            return null;
        }
    }
}
