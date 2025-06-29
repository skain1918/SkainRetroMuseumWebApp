using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Models;
using SkainRetroMuseumWebApp.Services;

namespace SkainRetroMuseumWebApp.Controllers;

public class HardwaresController : Controller {
    private HardwaresService _service;
    public HardwaresController(HardwaresService service) {
        _service = service;
    }
    public async Task<IActionResult> Index() {
        var allHardwares = await _service.GetAllAsync();
        return View(allHardwares);
    }
    public async Task<IActionResult> Create() {
        var hardwaresDropdownData = await _service.GetPlatformsAndBranchesAsync();
        ViewBag.Platforms = new SelectList(hardwaresDropdownData.Platforms, "Id", "Name");
        ViewBag.Branches = new SelectList(hardwaresDropdownData.Branches, "Id", "Name");
        return View();
    }
    public async Task<IActionResult> Detail(int id) {
        var hardware = await _service.GetDtoByIdAsync(id);
        if (hardware == null) {
            return View("NotFound");
        }
        return View(hardware);
    }
    public async Task<IActionResult> Edit(int id) {
        var hardwareToEdit = await _service.GetByIdAsync(id);
        if (hardwareToEdit == null) {
            return View("NotFound");
        }
        var response = new HardwareDTO {
            Id = hardwareToEdit.Id,
            Name = hardwareToEdit.Name,
            Manufacturer = hardwareToEdit.Manufacturer,
            Type = hardwareToEdit.Type,
            PlatformId = hardwareToEdit.Platform.Id,
            PlatformName = hardwareToEdit.Platform.Name,
            Condition = hardwareToEdit.Condition,
            YearOfManufactured = hardwareToEdit.YearOfManufactured,
            BranchId = hardwareToEdit.Branch.Id,
        };
        var hardwaresDropdownData = await _service.GetPlatformsAndBranchesAsync();
        ViewBag.Platforms = new SelectList(hardwaresDropdownData.Platforms, "Id", "Name");
        ViewBag.Branches = new SelectList(hardwaresDropdownData.Branches, "Id", "Name");
        return View(response);
    }
    public async Task<IActionResult> Delete(int id) {
        var hardware = await _service.GetDtoByIdAsync(id);
        if (hardware == null) {
            return View("NotFound");
        }
        return View(hardware);
    }

    [HttpPost]
    public async Task<IActionResult> Create(HardwareDTO newHardware) {
        if (ModelState.IsValid) {
            await _service.CreateAsync(newHardware);
            return RedirectToAction("Index");
        }
        var hardwaresDropdownData = await _service.GetPlatformsAndBranchesAsync();
            ViewBag.Platforms = new SelectList(hardwaresDropdownData.Platforms, "Id", "Name");
            ViewBag.Branches = new SelectList(hardwaresDropdownData.Branches, "Id", "Name");
            return View(newHardware);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, HardwareDTO hardware) {
        if(ModelState.IsValid) {
            await _service.UpdateAsync(id, hardware);
        return RedirectToAction("Index");
        }
        var hardwaresDropdownData = await _service.GetPlatformsAndBranchesAsync();
        ViewBag.Platforms = new SelectList(hardwaresDropdownData.Platforms, "Id", "Name");
        ViewBag.Branches = new SelectList(hardwaresDropdownData.Branches, "Id", "Name");
        return View(hardware);

    }
    [HttpPost]
    public async Task<IActionResult> DeleteSubmit(int id) {
        var hardwareToDelete = await _service.GetByIdAsync(id);
        if (hardwareToDelete == null) {
            return View("NotFound");
        }
        await _service.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
