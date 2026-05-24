namespace Persistence.Storage;

public sealed record R2StorageConfig
{
    public required string AccountId { get; init; }
    public required string AccessKeyId { get; init; }
    public required string SecretAccessKey { get; init; }
    public required string BucketName { get; init; }
    public required string PublicBaseUrl { get; init; }

    public string Endpoint => $"https://{AccountId}.r2.cloudflarestorage.com";
}