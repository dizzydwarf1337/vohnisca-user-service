namespace Application.Core.Storage;

public class StorageFileKey
{
    public static string ExtractKeyFromUrl(string url)
    {
        var uri = new Uri(url);
        return uri.AbsolutePath.TrimStart('/');
    }
}