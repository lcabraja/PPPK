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
    public class S3ObjectToExtension : IValueConverter {
        private S3Object? _obj;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is S3Object s3Object) {
                _obj = s3Object;
                return s3Object.GetName().Split(".").Last();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (_obj != null) {
                return _obj;
            }
            return null;
        }
    }
}
