using Amazon.S3;
using Amazon.S3.Model;
using Application.Interfaces.Storage;
using LanguageExt;
using LanguageExt.Common;

namespace Persistence.Storage;

public sealed class CloudflareR2BlobStorage(IAmazonS3 s3, R2StorageConfig config) : IBlobStorage
{
    public async Task<Either<Error, Uri>> SaveFileAsync(BlobUploadRequest request, CancellationToken ct = default)
    {
        try
        {
            await s3.PutObjectAsync(new PutObjectRequest
            {
                BucketName = config.BucketName,
                Key = request.Key,
                InputStream = request.Content,
                ContentType = request.ContentType,
                AutoCloseStream = false,
                DisablePayloadSigning = true
            }, ct);

            return new Uri($"{config.PublicBaseUrl.TrimEnd('/')}/{request.Key}");
        }
        catch (Exception ex)
        {
            return Error.New(ex.Message, ex);
        }
    }

    public async Task<Either<Error, Unit>> DeleteFileAsync(string key, CancellationToken ct = default)
    {
        try
        {
            await s3.DeleteObjectAsync(config.BucketName, key, ct);
            return Unit.Default;
        }
        catch (Exception ex)
        {
            return Error.New(ex.Message, ex);
        }
    }
}