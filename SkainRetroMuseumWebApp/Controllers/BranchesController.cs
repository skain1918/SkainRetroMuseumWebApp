using Microsoft.AspNetCore.Mvc;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Services;
using System.Threading.Tasks;

namespace SkainRetroMuseumWebApp.Controllers;

public class BranchesController : Controller
{
    private BranchesService _service;
    public BranchesController(BranchesService service)
    {
        _service = service;
    }
    public async Task<IActionResult> Index()
    {
        var allBranches = await _service.GetAllAsync();
        return View(allBranches);
    }
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(BranchDTO newBranch)
    {
        await _service.CreateAsync(newBranch);
        return RedirectToAction("Index");
    }
}
