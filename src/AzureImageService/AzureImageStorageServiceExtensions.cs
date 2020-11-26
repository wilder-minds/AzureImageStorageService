using System;
using System.Diagnostics.CodeAnalysis;
using WilderMinds.AzureImageStorageService;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class AzureImageStorageServiceExtensions
  {
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
