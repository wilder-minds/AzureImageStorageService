using System;

namespace WilderMinds.AzureImageStorageService
{
  /// <summary>
  /// A class that represents the response from a single image request.
  /// </summary>
  public class ImageResponse
  {
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ImageResponse"/> is success.
    /// </summary>
    /// <value>
    ///   <c>true</c> if success; otherwise, <c>false</c>.
    /// </value>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the image URL.
    /// </summary>
    /// <value>
    /// The image URL.
    /// </value>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [image changed].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [image changed]; otherwise, <c>false</c>.
    /// </value>
    public bool ImageChanged { get; set; }

    /// <summary>
    /// Gets or sets the exception.
    /// </summary>
    /// <value>
    /// The exception.
    /// </value>
    public Exception Exception { get; set; }
  }
}