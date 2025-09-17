using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Services;

namespace SkainRetroMuseumWebApp.Controllers; 
public class MessagesController : Controller {
    private MessagesService _service;
            public MessagesController(MessagesService service) {
        _service = service;
    }

    public async Task<IActionResult> Index() {
        var allMessages = await _service.GetAllAsync();
        return View(allMessages);
    }
    public async Task<IActionResult> GetMessageById(int id) {
                   var message = await _service.GetByIdAsync(id);
        if (message == null) {
            return View("NotFound");
        }
        return View(message);
    }
    public async Task<IActionResult> Create() {
        return View();
    }
    [Authorize(Roles = "kurator, obsluha")]
    public async Task<IActionResult> Delete(int id) {
        return await GetMessageById(id);
    }
    [HttpPost]
    public async Task<IActionResult> Create(MessageDTO newMessage) {
        if (ModelState.IsValid) {
            await _service.CreateAsync(newMessage);
            return RedirectToAction("Index");
        }
        return View(newMessage);
    }
    [HttpPost]
    [Authorize(Roles = "kurator, obsluha")]
    public async Task<IActionResult> DeleteSubmit(int id) {
        var messageToDelete = await _service.GetByIdAsync(id);
        if (messageToDelete == null) {
            return View("NotFound");
        }
        await _service.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
