using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;

namespace WilderMinds.AzureImageStorageService
{
  /// <summary>
  /// A service for storing and updating images in the Azure Storage Blog service.
  /// </summary>
  /// <seealso cref="WilderMinds.AzureImageService.IImageStorageService" />
  public class AzureImageStorageService : IAzureImageStorageService
  {
    private readonly ILogger<AzureImageStorageService> _logger;
    private readonly AzureImageStorageServiceClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageStorageService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
      public AzureImageStorageService(ILogger<AzureImageStorageService> logger, 
        AzureImageStorageServiceClient client)
      {
        _logger = logger;
        _client = client;
      }

    /// <summary>
    /// Stores the image in Azure Storage Blog Service.
    /// </summary>
    /// <param name="storeImagePath">The store image path.</param>
    /// <param name="imageStream">The image stream.</param>
    /// <returns></returns>
    public async Task<ImageResponse> StoreImage(string containerName, string storeImagePath, Stream imageStream)
    {

      var response = new ImageResponse();

      try
      {
        var container = _client.GetBlobContainerClient(containerName);

        // Get old Image to update
        var blob = container.GetBlobClient(storeImagePath);
        bool shouldUpload = true;
        if (await blob.ExistsAsync())
        {
          var props = await blob.GetPropertiesAsync();
          if (props.Value.ContentLength == imageStream.Length)
          {
            shouldUpload = false;
            response.ImageChanged = false;
            response.Success = true;
          }
        }

        if (shouldUpload)
        {
          var result = await blob.UploadAsync(imageStream, true);
          if (result != null)
          {
            response.ImageChanged = true;
            response.Success = true;
          }
        }

        // Update the Image Url in every case
        response.ImageUrl = new Uri(container.Uri, string.Concat(containerName, "/", blob.Name)).ToString();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to upload blob: {ex}");
        response.Success = false;
        response.Exception = ex;
      }
      finally
      {
        if (imageStream != null) imageStream.Dispose();
      }

      return response;
    }

    /// <summary>
    /// Stores the image.
    /// </summary>
    /// <param name="storeImagePath">The store image path.</param>
    /// <param name="imageData">The image data.</param>
    /// <returns></returns>
    public Task<ImageResponse> StoreImage(string containerName, string storeImagePath, byte[] imageData)
    {
      var stream = new MemoryStream(imageData);
      return StoreImage(containerName, storeImagePath, stream);
    }

  }
}
