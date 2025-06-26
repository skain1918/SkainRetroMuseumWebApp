using Microsoft.AspNetCore.Mvc;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Services;

namespace SkainRetroMuseumWebApp.Controllers; 
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
    public async Task<IActionResult> Detail(int id)
    {
        var platform = await _service.GetByIdAsync(id);
        if (platform == null)
        {
            return NotFound();
        }
        return View(platform);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var platform = await _service.GetByIdAsync(id);
        if (platform == null)
        {
            return NotFound();
        }
        return View(platform);
    }
    [HttpPost]
    public async Task<IActionResult> Create(PlatformDTO newPlatform)
    {
        await _service.CreateAsync(newPlatform);
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, PlatformDTO platform)
    {
        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(platform);
            return RedirectToAction("Index");
        }
        return View(platform);

    }
}