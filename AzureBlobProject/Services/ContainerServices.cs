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
        public async Task<List<string>> GetAllContainerAndBlobs()
        {
            List<string> ContainerAndBlobName = new();
            ContainerAndBlobName.Add("---AccountInfo Name: " + _blobClient.AccountName + "-------");
            ContainerAndBlobName.Add("-----------------------------------------------------------");

            await foreach (BlobContainerItem blobContainerItem in _blobClient.GetBlobContainersAsync())
            {
                ContainerAndBlobName.Add("----" + blobContainerItem.Name);
                BlobContainerClient _blobContainer = _blobClient.GetBlobContainerClient(blobContainerItem.Name);
                await foreach(BlobItem blobItem in _blobContainer.GetBlobsAsync())
                {
                    ContainerAndBlobName.Add("--" + blobItem.Name);
                }
                ContainerAndBlobName.Add("-----------------------------------------------------------");
            }
            return ContainerAndBlobName;
        }
    }
}
