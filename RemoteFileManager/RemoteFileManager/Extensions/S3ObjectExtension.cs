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
        public static string GetName(this S3Object s3Object, string directory)
            => s3Object.Key[directory.Length..];
        public static IList<S3Object> GetDirectoryContents(this IList<S3Object> s3Objects, string directory, bool includeDirectories = false)
            => s3Objects
                .Where(r => r.Key.Length > directory.Length)
                .Where(r => !r.Key[directory.Length..^1].Contains('/'))
                .Where(r => includeDirectories || r.Key[^1] != '/')
                .ToList();

        public static IList<string> GetDirectories(this IList<S3Object> s3Objects) {
            IList<string> directories = new List<string>();
            s3Objects.Where(s => s.Key.Contains('/'))
                .ToList()
                .ForEach(s => {
                    string directory = s.Key;
                    do {
                        directory = directory[0..(directory[0..^1].LastIndexOf("/") + 1)];
                        if (!directories.Contains(directory)) {
                            directories.Add(directory);
                        }
                    } while (directory[0..^1].Contains('/'));
                });
            return directories;
        }
    }
}
