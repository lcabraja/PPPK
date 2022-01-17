
// .ToList().ForEach(Console.WriteLine);

using Amazon.S3.Model;
using S3Repository;
using Tester;

void SeparateLines() {
    Console.WriteLine();
    Console.WriteLine(new string('*', Console.BufferWidth));
};

AWSBucketRepository bucket = new AWSBucketRepository(
    "hr.algebra.lcabraja.testbucket",
    "AKIAWLDXOVJS5ZCYLFHH",
    "8X4/XMtKRIm9FM/RiSqmUrqG1CF0uux+pKFUjmgc"
);

var token = new CancellationToken();
IList<S3Object> s3Objects;

s3Objects = bucket.GetObjectsInBucketAsync(token).GetAwaiter().GetResult();
s3Objects.ListItems();
SeparateLines();

bucket.GetObjectsInPathAsync("", token).GetAwaiter().GetResult().ListItems();
SeparateLines();

IList<string> directories = new List<string>();
s3Objects.ToList().ForEach(s => {
    if (s.Key.Contains('/')) {
        string directory = s.Key;
        do {
            directory = directory[0..(directory[0..^1].LastIndexOf("/") + 1)];
            if (!directories.Contains(directory)) {
                global::System.Console.WriteLine(directory);
                directories.Add(directory);
            }
        } while (directory[0..^1].Contains('/'));
    }
});

directories.OrderBy(dir => dir).ToList().ForEach(dir => {
    global::System.Console.WriteLine($"\n{dir}");
    var result = bucket.GetObjectsInPathAsync(dir, token).GetAwaiter().GetResult();
    result
        .Where(r => r.Key.Length > dir.Length)
        .Where(r => !r.Key[dir.Length..^1].Contains('/'))
        .ToList()
        .ForEach(r => {
            var item = r.Key[dir.Length..];
            global::System.Console.WriteLine($"  {item} # {item.Length}");
        });
});
SeparateLines();

bucket.GetObjectsInPathAsync("what/are/you/looking/aglbera_waifu2x_4096x4096_2n_png.png", token).GetAwaiter().GetResult().ListItems();
