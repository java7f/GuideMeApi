using Azure.Storage.Blobs;
using GuideMe.Interfaces.AzureBlob;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Uploads a profile photo to the Azure Blob store container "profile-photo"
        /// </summary>
        /// <param name="model">The profile photo file</param>
        /// <returns>The absolute URI for the uploaded photo</returns>
        public async Task<string> UploadProfilePhoto(IFormFile model)
        {
            return await Upload(model, _blobStoreSettings.ProfilePhotoContainer);
        }

        /// <summary>
        /// Uploads an audiofile to the Azure Blob store container "audiofiles"
        /// </summary>
        /// <param name="model">The new audiofile</param>
        /// <returns>The absolute URI for the uploaded audiofile</returns>
        public async Task<string> UploadAudiofile(IFormFile model)
        {
            return await Upload(model, _blobStoreSettings.AudiofilesContainer);
        }

        /// <summary>
        /// Deletes the specified audiofile from the Azure Blob store container "audiofiles"
        /// </summary>
        /// <param name="filename">The name of the file to delete</param>
        /// <returns>True if successfully deleted</returns>
        public async Task<bool> DeleteAudiofile(string filename)
        {
            return await Delete(filename, _blobStoreSettings.AudiofilesContainer);
        }

        /// <summary>
        /// Uploads a location photo to the Azure Blob store container "location-photos"
        /// </summary>
        /// <param name="model">The location photo</param>
        /// <returns>The absolute URI for the uploaded photo</returns>
        public async Task<string> UploadLocationPhoto(IFormFile model)
        {
            return await Upload(model, _blobStoreSettings.LocationPhotosContainer);
        }

        /// <summary>
        /// Uploads the given file to the specified container in the Azure Blob store
        /// </summary>
        /// <param name="model">The file to be uploaded</param>
        /// <param name="container">The name of the container</param>
        /// <returns>The absolute URI of the upleaded file</returns>
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

        /// <summary>
        /// Deletes from the Blob Store the specified file from the specifed container
        /// </summary>
        /// <param name="fileName">The name of the file to delete</param>
        /// <param name="container">The container where the file is</param>
        /// <returns>True if successfully deleted</returns>
        private async Task<bool> Delete(string fileName, string container)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            return await blobContainer.DeleteBlobIfExistsAsync(fileName);
        }

        public IFormFile ConvertBase64ToFormFile(string base64File,string name, string fileName)
        {
            byte[] bytes = Convert.FromBase64String(base64File);
            MemoryStream stream = new MemoryStream(bytes);

            IFormFile file = new FormFile(stream, 0, bytes.Length, name, fileName);
            return file;
        }
    }
}
