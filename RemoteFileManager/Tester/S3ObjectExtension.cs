using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester {
    public static class S3ObjectExtension {
        public static string GetName(this S3Object s3Object) {
            int location = s3Object.Key.LastIndexOf("\\");
            location = location > 0 ? location : 0;
            return s3Object.Key[location..];
        }
        public static void ListItems(this IList<S3Object> s3Objects)
            => s3Objects.ToList().ForEach(x => Console.WriteLine(x.GetName()));
    }
}
