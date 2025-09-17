using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Models;
using SkainRetroMuseumWebApp.Services;

namespace SkainRetroMuseumWebApp.Controllers;
public class PlatformsController : Controller {
    private PlatformsService _service;
    public PlatformsController(PlatformsService service) {
        _service = service;
    }
    public async Task<IActionResult> Index() {
        var allPlatforms = await _service.GetAllAsync();
        return View(allPlatforms);
    }
    [Authorize(Roles = "kurator, obsluha")]

    public async Task<IActionResult> Create() {
        return View();
    }
    public async Task<IActionResult> Detail(int id) {
        return await GetPlatformById(id);
    }

    private async Task<IActionResult> GetPlatformById(int id) {
        var platform = await _service.GetByIdAsync(id);
        if (platform == null) {
            return View("NotFound");
        }
        return View(platform);
    }
    [Authorize(Roles = "kurator, obsluha")]

    public async Task<IActionResult> Edit(int id) {
        return await GetPlatformById(id);
    }
    [Authorize(Roles = "kurator")]

    public async Task<IActionResult> Delete(int id) {
        return await GetPlatformById(id);
    }
    [Authorize(Roles = "kurator, obsluha")]
    [HttpPost]
    public async Task<IActionResult> Create(PlatformDTO newPlatform) {
        if (ModelState.IsValid) {
            await _service.CreateAsync(newPlatform);
            return RedirectToAction("Index");
        }
        return View(newPlatform);
    }
    [Authorize(Roles = "kurator, obsluha")]
    [HttpPost]
    public async Task<IActionResult> Edit(int id, PlatformDTO platform) {
        if (ModelState.IsValid) {
            await _service.UpdateAsync(platform);
            return RedirectToAction("Index");
        }
        return View(platform);
    }
    [Authorize(Roles = "kurator")]
    [HttpPost]
    public async Task<IActionResult> DeleteSubmit(int id) {
        var platformToDelete = await _service.GetByIdAsync(id);
        if (platformToDelete == null) {
            return View("NotFound");
        }
        await _service.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}