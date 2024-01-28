# WilderMinds.AzureImageStorageService

Simple project to wrap the Azure Storage Service to simplify storing images in Azure Storage Blob storage. 

To install, 

```
> dotnet add package WilderMinds.AzureImageStorageService
```

```cs
// In Configure Services
if (_env.IsDevelopment())
{
  // For development you can just mock the interface
  svcs.AddTransient<IAzureImageService, FakeImageService>(); 
}
else
{
  // Supply your account, API Key, and the url for the blobs
  svcs.AddAzureImageService(_config["BlobStorage:Account"], 
    _config["BlobStorage:Key"], 
    _config["BlobStorage:ServiceUrl"]);
}
```

Once you have this working, you can simply use the service:

```cs
var response = 
  await _imageStorageService.StoreImage(
    "img", 
    "file.jpg", 
    someImageBuffer);

if (response.Success) return Results.Ok();
```