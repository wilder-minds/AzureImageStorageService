using Azure.Storage;
using System;

namespace WilderMinds.AzureImageStorageService
{
  public class AzureImageStorageCredentials : StorageSharedKeyCredential
  {
    public AzureImageStorageCredentials(string accountName, string accountKey, string url)
      :base(accountName, accountKey)
    {
      AccountUrl = new Uri(url);
    }

    public Uri AccountUrl { get; private set; }

  }
}
