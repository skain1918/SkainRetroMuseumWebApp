using Microsoft.AspNetCore.Mvc;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Services;

namespace SkainRetroMuseumWebApp.Controllers {
    public class PlatformsController : Controller
    {
        private PlatformsService _service;
        public PlatformsController(PlatformsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allPlatforms = await _service.GetAllAsync();
            return View(allPlatforms);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PlatformDTO newPlatform)
        {
            await _service.CreateAsync(newPlatform);
            return RedirectToAction("Index");
        }

    }
}
