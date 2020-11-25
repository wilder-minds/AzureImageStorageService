using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;

namespace WilderMinds.AzureImageService
{
  /// <summary>
  /// A service for storing and updating images in the Azure Storage Blog service.
  /// </summary>
  /// <seealso cref="WilderMinds.AzureImageService.IImageStorageService" />
  public class ImageStorageService : IImageStorageService
  {
    private readonly ILogger<ImageStorageService> _logger;
    private readonly ImageStorageServiceClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageStorageService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ImageStorageService(ILogger<ImageStorageService> logger, ImageStorageServiceClient client)
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
    public async Task<ImageResponse> StoreImage(string storeImagePath, Stream imageStream)
    {

      var response = new ImageResponse();

      try
      {
        var imageName = Path.GetFileName(storeImagePath);
        var imagePath = Path.GetFullPath(storeImagePath);

        var container = _client.GetBlobContainerClient(imagePath);

        // Get old Image to update
        var blob = container.GetBlobClient(imageName);
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
            response.ImageUrl = blob.Uri.AbsoluteUri;
          }
        }

      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to upload blob: {ex}");
        response.Success = false;
        response.Exception = ex;
      }

      return response;
    }

    /// <summary>
    /// Stores the image.
    /// </summary>
    /// <param name="storeImagePath">The store image path.</param>
    /// <param name="imageData">The image data.</param>
    /// <returns></returns>
    public Task<ImageResponse> StoreImage(string storeImagePath, byte[] imageData)
    {
      using (var stream = new MemoryStream(imageData))
      {
        return StoreImage(storeImagePath, stream);
      }
    }

  }
}
