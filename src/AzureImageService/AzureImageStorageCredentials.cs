using Azure.Storage;

namespace WilderMinds.AzureImageStorageService
{
  public class AzureImageStorageCredentials : StorageSharedKeyCredential
  {
    public AzureImageStorageCredentials(string accountName, string accountKey, string url)
      :base(accountName, accountKey)
    {
      AccountUrl = url;
    }

    public string AccountUrl { get; private set; }

  }
}
