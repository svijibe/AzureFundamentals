using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobProject.Services
{
    public class ContainerServices : IContainerServices
    {
        private readonly BlobServiceClient _blobClient;
        public ContainerServices(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }
        public async Task CreateContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
        }
        public async Task DeleteContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            await blobContainerClient.DeleteIfExistsAsync();
        }
        public async Task<List<string>> GetAllContainer()
        {
            List<string> ContainerName = new();

            await foreach( BlobContainerItem blobContainerItem in _blobClient.GetBlobContainersAsync())
            {
                ContainerName.Add(blobContainerItem.Name);
            }
            return ContainerName;
        }
        public Task<List<string>> GetAllContainerAndBlobs()
        {
            throw new NotImplementedException();
        }
    }
}
