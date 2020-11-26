using System;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using WilderMinds.AzureImageStorageService;
using Xunit;

namespace AzureImageService.Tests
{
  public class AzureImageStorageServiceTests
  {
    private IAzureImageStorageService _imageStorageService;

    public AzureImageStorageServiceTests()
    {
      var testKey = Encoding.UTF8.GetBytes("TESTME");
      var credentials = new AzureImageStorageCredentials("foo", 
        Convert.ToBase64String(testKey), 
        "https://azurestorage");
      var client = new AzureImageStorageServiceClient(credentials);
      _imageStorageService = new AzureImageStorageService(new NullLogger<AzureImageStorageService>(), client);
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
