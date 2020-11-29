using System;
using System.Diagnostics.CodeAnalysis;
using WilderMinds.AzureImageStorageService;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class AzureImageStorageServiceExtensions
  {
    /// <summary>
    /// Adds the azure image storage service to the dependency injection layer.
    /// </summary>
    /// <param name="coll">The coll.</param>
    /// <param name="azureAccountName">Name of the azure account.</param>
    /// <param name="azureAccountKey">The azure account key (API Key).</param>
    /// <param name="azureStorageUrl">The azure storage URL.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Azure Account Key must be a Base64 Encoded String. If running in development, mock the IImageStorageService for development instead.</exception>
    public static IServiceCollection AddAzureImageStorageService(this IServiceCollection coll, 
      [NotNull] string azureAccountName,
      [NotNull] string azureAccountKey,
      [NotNull] string azureStorageUrl)
    {
      // Test for valid Base64 Key
      Span<byte> buffer = new Span<byte>(new byte[azureAccountKey.Length]);
      if (!Convert.TryFromBase64String(azureAccountKey, buffer, out int bytesParsed))
      {
        throw new InvalidOperationException("Azure Account Key must be a Base64 Encoded String. If running in development, mock the IImageStorageService for development instead.");
      }

      coll.AddScoped(coll => new AzureImageStorageCredentials(azureAccountName, azureAccountKey, azureStorageUrl));
      coll.AddScoped<AzureImageStorageServiceClient>();
      coll.AddTransient<IAzureImageStorageService, AzureImageStorageService>();

      return coll;
    }
  }
}
