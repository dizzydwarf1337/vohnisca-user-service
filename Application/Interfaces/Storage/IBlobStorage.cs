using LanguageExt;
using LanguageExt.Common;

namespace Application.Interfaces.Storage;

public interface IBlobStorage
{
    Task<Either<Error, Uri>> SaveFileAsync(BlobUploadRequest request, CancellationToken ct = default);
    Task<Either<Error, Unit>> DeleteFileAsync(string key, CancellationToken ct = default);
}

public sealed record BlobUploadRequest(
    string Key,
    Stream Content,
    string ContentType
);