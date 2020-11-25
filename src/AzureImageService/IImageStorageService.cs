using System.IO;
using System.Threading.Tasks;

namespace WilderMinds.AzureImageService
{
  /// <summary>
  /// Interface that represents the Image Storage Service's public API
  /// </summary>
  public interface IImageStorageService
  {
    /// <summary>
    /// Stores the image.
    /// </summary>
    /// <param name="storageImagePath">The storage image path.</param>
    /// <param name="imageStream">The image stream.</param>
    /// <returns></returns>
    Task<ImageResponse> StoreImage(string storageImagePath, Stream imageStream);

    /// <summary>
    /// Stores the image.
    /// </summary>
    /// <param name="storeImagePath">The store image path.</param>
    /// <param name="imageData">The image data.</param>
    /// <returns></returns>
    Task<ImageResponse> StoreImage(string storeImagePath, byte[] imageData);
  }
}