using AzureBlobProject.Models;
using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureBlobProject.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobService _blobService;
        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }
        [HttpGet]
        public async Task<IActionResult> Manage(string containerName)
        {
            var blobObj = await _blobService.GetAllBlobs(containerName);
            return View(blobObj);
        }
        [HttpGet]
        public async Task<IActionResult> AddFile(string containerName)
        {
            
            return View();
        }


    }
}
