using AzureBlobProject.Models;
using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AzureBlobProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContainerServices _containerServices;
        private readonly IBlobService _blobService;
        public HomeController(ILogger<HomeController> logger, IContainerServices containerService, IBlobService blobService)
        {
            _logger = logger;
            _containerServices = containerService;
            _blobService = blobService;
        }

        public IActionResult Index()
        {
            return View(_containerServices.GetAllContainerAndBlobs().GetAwaiter().GetResult());
        }

        public async Task<IActionResult> Images()
        {
            return View(_blobService.GetAllBlobsWithUri("azuredemo-private").GetAwaiter().GetResult()) ;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
