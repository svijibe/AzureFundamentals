﻿using AzureBlobProject.Models;

namespace AzureBlobProject.Services
{
    public interface IBlobService
    {
        Task<List<string>> GetAllBlobs(string containerName);
        Task<List<BlobModel>> GetAllBlobsWithUri(string containerName);
        Task<string> GetBlob(string name, string containerName);
        Task<bool> CreateBlob(string name, IFormFile file,  string containerName, BlobModel blobModel);
        Task<bool> DeleteBlob(string name, string containerName);
    }
}
