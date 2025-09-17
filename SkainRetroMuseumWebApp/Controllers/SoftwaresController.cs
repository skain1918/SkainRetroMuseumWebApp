using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Models;
using SkainRetroMuseumWebApp.Services;

namespace SkainRetroMuseumWebApp.Controllers;
public class SoftwaresController : Controller {
    public SoftwaresService _service;
    public SoftwaresController(SoftwaresService service) {
        _service = service;
    }

    public async Task<IActionResult> Index() {
        var allSoftwares = await _service.GetAllAsync();
        return View(allSoftwares);
    }
    [Authorize(Roles = "kurator, obsluha")]
    public async Task<IActionResult> Create() {
        var softwaresDropdownData = await _service.GetPlatformsAsync();
        ViewBag.Platforms = new SelectList(softwaresDropdownData.Platforms, "Id", "Name");
        return View();
    }
    public async Task<IActionResult> Detail(int id) {
        var software = await _service.GetDtoByIdAsync(id);
        if (software == null) {
            return View("NotFound");
        }
        return View(software);
    }
    [Authorize(Roles = "kurator, obsluha")]
    public async Task<IActionResult> Edit(int id) {
        var softwareToEdit = await _service.GetByIdAsync(id);
        if (softwareToEdit == null) {
            return View("NotFound");
        }
        var response = new SoftwareDTO {
            Id = softwareToEdit.Id,
            Name = softwareToEdit.Name,
            Year = softwareToEdit.Year,
            PlatformId = softwareToEdit.Platform.Id,
            PlatformName = softwareToEdit.Platform.Name,
            Description = softwareToEdit.Description,
        };
        var softwaresDropdownData = await _service.GetPlatformsAsync();
        ViewBag.Platforms = new SelectList(softwaresDropdownData.Platforms, "Id", "Name");
        return View(response);
    }
    [Authorize(Roles = "kurator")]
    public async Task<IActionResult> Delete(int id) {
        var software = await _service.GetDtoByIdAsync(id);
        if (software == null) {
            return View("NotFound");
        }
        return View(software);
    }
    [Authorize(Roles = "kurator, obsluha")]
    [HttpPost]
    public async Task<IActionResult> Create(SoftwareDTO newSoftware) {
        if (ModelState.IsValid) {
            await _service.CreateAsync(newSoftware);
        return RedirectToAction("Index");
        }
        var softwaresDropdownData = await _service.GetPlatformsAsync();
        ViewBag.Platforms = new SelectList(softwaresDropdownData.Platforms, "Id", "Name");
        return View(newSoftware);
    }
    [Authorize(Roles = "kurator, obsluha")]
    [HttpPost]
    public async Task<IActionResult> Edit(int id, SoftwareDTO software) {
        if (ModelState.IsValid) {
            await _service.UpdateAsync(id, software);
            return RedirectToAction("Index");
        }
        var softwaresDropdownData = await _service.GetPlatformsAsync();
        ViewBag.Platforms = new SelectList(softwaresDropdownData.Platforms, "Id", "Name");
        return View(software);
    }
    [Authorize(Roles = "kurator")]
    [HttpPost]
    public async Task<IActionResult> DeleteSubmit(int id) {
        var softwareToDelete = await _service.GetByIdAsync(id);
        if (softwareToDelete == null) {
            return View("NotFound");
        }
        await _service.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}