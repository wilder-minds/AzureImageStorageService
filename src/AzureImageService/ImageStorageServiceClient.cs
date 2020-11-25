using System;
using Azure.Storage.Blobs;

namespace WilderMinds.AzureImageService
{
  public class ImageStorageServiceClient : BlobServiceClient
  {
    public ImageStorageServiceClient(ImageStorageCredentials credentials)
    {
      Client = new BlobServiceClient(new Uri(credentials.AccountUrl), credentials);
    }

    public BlobServiceClient Client { get; protected set; }
  }
}
