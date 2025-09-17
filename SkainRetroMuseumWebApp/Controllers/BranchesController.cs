using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Models;
using SkainRetroMuseumWebApp.Services;
using System.Threading.Tasks;

namespace SkainRetroMuseumWebApp.Controllers;

public class BranchesController : Controller {
    private BranchesService _service;
    public BranchesController(BranchesService service) {
        _service = service;
    }
    public async Task<IActionResult> Index() {
        var allBranches = await _service.GetAllAsync();
        return View(allBranches);
    }
    [Authorize(Roles = "kurator, obsluha")]
    public async Task<IActionResult> Create() {
        return View();
    }
    public async Task<IActionResult> Detail(int id) {
        return await getBranchById(id);
    }
    private async Task<IActionResult> getBranchById(int id) {
        var branch = await _service.GetByIdAsync(id);
        if (branch == null) {
            return View("NotFound");
        }
        return View(branch);
    }
    public async Task<IActionResult> Edit(int id) {
        return await getBranchById(id);
    }
    [Authorize(Roles = "kurator")]
    public async Task<IActionResult> Delete(int id) {
        return await getBranchById(id);
    }
    [Authorize(Roles = "kurator, obsluha")]
    [HttpPost]
    public async Task<IActionResult> Create(BranchDTO newBranch) {
        if (ModelState.IsValid) {
            await _service.CreateAsync(newBranch);
            return RedirectToAction("Index");
        }
        return View(newBranch);
    }
    [Authorize(Roles = "kurator, obsluha")]
    [HttpPost]
    public async Task<IActionResult> Edit(int id, BranchDTO branch) {
        if (ModelState.IsValid) {
            await _service.UpdateAsync(branch);
            return RedirectToAction("Index");
        }
        return View(branch);
    }
    [Authorize(Roles = "kurator")]
    [HttpPost]
    public async Task<IActionResult> DeleteSubmit(int id) {
        var branchToDelete = await _service.GetByIdAsync(id);
        if (branchToDelete == null) {
            return View("NotFound");
        }
        await _service.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
