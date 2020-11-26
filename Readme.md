# WilderMinds.AzureImageStorageService

Simple project to wrap the Azure Storage Service to simplify storing images in Azure Storage Blob storage. 

To install, 

```
> dotnet add package WilderMinds.AzureImageStorageService
```

```
      // In Configure Services
      if (_env.IsDevelopment())
      {
        // For development you can jsut mock the interface
        svcs.AddTransient<IAzureImageService, FakeImageService>(); 
      }
      else
      {
        svcs.AddAzureImageService(_config["BlobStorage:Account"], 
          _config["BlobStorage:Key"], 
          _config["BlobStorage:ServiceUrl"]);
      }

```