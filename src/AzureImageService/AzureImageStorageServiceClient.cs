using System;
using Azure.Storage.Blobs;

namespace WilderMinds.AzureImageStorageService
{
  public class AzureImageStorageServiceClient : BlobServiceClient
  {
    public AzureImageStorageServiceClient(AzureImageStorageCredentials credentials)
    {
      Client = new BlobServiceClient(new Uri(credentials.AccountUrl), credentials);
    }

    public BlobServiceClient Client { get; protected set; }
  }
}
