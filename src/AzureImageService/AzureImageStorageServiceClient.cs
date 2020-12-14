using System;
using Azure.Storage.Blobs;

namespace WilderMinds.AzureImageStorageService
{
  public class AzureImageStorageServiceClient : BlobServiceClient
  {
    public AzureImageStorageServiceClient(AzureImageStorageCredentials credentials) 
      : base(credentials.AccountUrl, credentials)
    {
    }

  }
}
