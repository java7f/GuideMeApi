using Azure.Storage.Blobs;
using GuideMe.Interfaces.AzureBlob;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Services
{
    /// <summary>
    /// Service that manages file storage operations in the Azure Blob Store 
    /// for the GuideMe project
    /// </summary>
    public class FileManagerService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IBlobStoreSettings _blobStoreSettings;
        public FileManagerService(IBlobStoreSettings blobStoreSettings, BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _blobStoreSettings = blobStoreSettings;
        }

        public async Task<string> UploadProfilePhoto(IFormFile model)
        {
            return await Upload(model, _blobStoreSettings.ProfilePhotoContainer);
        }

        private async Task<string> Upload(IFormFile model, string container)
        {
            try
            {
                var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
                var blobClient = blobContainer.GetBlobClient(model.FileName);
                await blobClient.UploadAsync(model.OpenReadStream());
                return blobClient.Uri.AbsoluteUri;
            }
            catch(Exception e)
            {
                throw (e);
            }
        }
    }
}
