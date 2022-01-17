// .ToList().ForEach(Console.WriteLine);

using Amazon.S3.Model;
using S3Repository;
using Tester;


AWSBucketRepository bucket = new AWSBucketRepository(
    "hr.algebra.lcabraja.testbucket",
    "AKIAWLDXOVJS4ALQLQR4",
    "tKRPcNE+HLicT0b9GRPKXY178IrBZRgeuYTOaQ9j"
);

var token = new CancellationToken();
IList<S3Object> s3Objects;

s3Objects = bucket.GetObjectsInBucketAsync(token).GetAwaiter().GetResult();
s3Objects.ListItems();
Console.WriteLine("\n\n");
s3Objects = bucket.GetObjectsInPathAsync("what/are/you/", token).GetAwaiter().GetResult();
s3Objects.ListItems();

