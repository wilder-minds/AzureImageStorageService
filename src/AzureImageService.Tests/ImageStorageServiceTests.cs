using System;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using WilderMinds.AzureImageService;
using Xunit;

namespace AzureImageService.Tests
{
  public class ImageStorageServiceTests
  {
    private IImageStorageService _imageStorageService;

    public ImageStorageServiceTests()
    {
      var testKey = Encoding.UTF8.GetBytes("TESTME");
      var credentials = new ImageStorageCredentials("foo", 
        Convert.ToBase64String(testKey), 
        "https://azurestorage");
      var client = new ImageStorageServiceClient(credentials);
      _imageStorageService = new ImageStorageService(new NullLogger<ImageStorageService>(), client);
    }

    [Fact]
    public async void ShouldFailIfEmptyRequest()
    {
      var response = await _imageStorageService.StoreImage("", new byte[] { });
      Assert.False(response.Success);
    }

    [Fact]
    public async void ShouldFailIfBadConfiguration()
    {
      var response = await _imageStorageService.StoreImage("file.jpg", new byte[] { 05, 05, 05 });
      Assert.False(response.Success);
    }
  }
}
