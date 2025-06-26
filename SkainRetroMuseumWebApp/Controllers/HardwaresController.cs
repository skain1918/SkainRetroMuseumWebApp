using Microsoft.AspNetCore.Mvc;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Services;

namespace SkainRetroMuseumWebApp.Controllers;

public class HardwaresController : Controller
{
    private HardwaresService _service;
    public HardwaresController(HardwaresService service)
    {
        _service = service;
    }
    public async Task<IActionResult> Index()
    {
        var allHardwares = await _service.GetAllAsync();
        return View(allHardwares);
    }
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(HardwareDTO newHardware)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateAsync(newHardware);
            return RedirectToAction("Index");
        }
        return View(newHardware);
    }
}
