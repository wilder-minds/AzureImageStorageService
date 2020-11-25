using Microsoft.Extensions.DependencyInjection;

namespace WilderMinds.AzureImageService
{
  public static class ImageStorageServiceExtensions
  {
    public static IServiceCollection AddImageStorageService(this IServiceCollection coll, 
      string azureAccountName, 
      string azureAccountKey, 
      string azureStorageUrl)
    {
      coll.AddScoped<ImageStorageCredentials>(coll => new ImageStorageCredentials(azureAccountName, azureAccountKey, azureStorageUrl));
      coll.AddScoped<ImageStorageServiceClient>();
      coll.AddTransient<IImageStorageService, ImageStorageService>();

      return coll;
    }
  }
}
