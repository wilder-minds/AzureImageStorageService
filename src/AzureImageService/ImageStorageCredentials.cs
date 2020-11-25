using Azure.Storage;

namespace WilderMinds.AzureImageService
{
  public class ImageStorageCredentials : StorageSharedKeyCredential
  {
    public ImageStorageCredentials(string accountName, string accountKey, string url)
      :base(accountName, accountKey)
    {
      AccountUrl = url;
    }

    public string AccountUrl { get; private set; }

  }
}
