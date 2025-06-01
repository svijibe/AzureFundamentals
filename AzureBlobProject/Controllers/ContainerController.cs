using AzureBlobProject.Models;
using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureBlobProject.Controllers
{
    public class ContainerController : Controller
    {
        private readonly IContainerServices _containerServices;
        public ContainerController(IContainerServices containerService)
        {
            _containerServices = containerService;
        }
        public async Task<IActionResult> Index()
        {
            var allContainer = await _containerServices.GetAllContainer();
            return View(allContainer);
        }

        public async Task<IActionResult> Create()
        {
            return View(new ContainerModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ContainerModel Container)
        {
            await _containerServices.CreateContainer(Container.Name);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(string containerName)
        {
            await _containerServices.DeleteContainer(containerName);
            return RedirectToAction(nameof(Index));
        }


    }
}
