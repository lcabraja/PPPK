using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteFileManager.Extensions {
    static class S3ObjectExtension {
        public static string GetName(this S3Object s3Object) {
            int location = s3Object.Key.LastIndexOf("\\");
            location = location > 0 ? location : 0;
            return s3Object.Key[location..];
        }
    }
}
