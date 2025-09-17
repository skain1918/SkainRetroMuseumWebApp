﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkainRetroMuseumWebApp.Models;

namespace SkainRetroMuseumWebApp.Controllers;
[Authorize(Roles = "kurator")]
public class RolesController : Controller {
    private RoleManager<IdentityRole> _roleManager;
    private UserManager<AppUser> _userManager;
    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager) {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public IActionResult Index() {
        return View(_roleManager.Roles);
    }
    public IActionResult Create() {
        return View();
    }
    public async Task<IActionResult> Edit(string id) {
        IdentityRole role = await _roleManager.FindByIdAsync(id);
        List<AppUser> members = new List<AppUser>();
        List<AppUser> nonMembers = new List<AppUser>();
        foreach (AppUser user in _userManager.Users) {
            var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
            list.Add(user);
        }
        return View(new RoleEdit {
            Role = role,
            Members = members,
            NonMembers = nonMembers
        });
    }


    [HttpPost]
    public async Task<IActionResult> Create(string name) {
        if (ModelState.IsValid) {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded) {
                return RedirectToAction("Index");
            }
            else {
                Errors(result);
            }
        }
        return View(name);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(string id) {
        IdentityRole role = await _roleManager.FindByIdAsync(id);
        if (role != null) {
            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) {
                return RedirectToAction("Index");
            }
            else {
                Errors(result);
            }
        }
        else {
            ModelState.AddModelError("", "No role found");
        }
        return View("Index", _roleManager.Roles);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RoleModification model) {
        IdentityResult result;
        if (ModelState.IsValid) {
            foreach (string userId in model.AddIds ?? new string[] { }) {
                AppUser user = await _userManager.FindByIdAsync(userId);
                if (user != null) {
                     result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded) {
                        Errors(result);
                    }
                }
            }
            foreach (string userId in model.DeleteIds ?? new string[] { }) {
                AppUser user = await _userManager.FindByIdAsync(userId);
                if (user != null) {
                    result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded) {
                        Errors(result);
                    }
                }
            }
        }
            if (ModelState.IsValid) {
                return RedirectToAction(nameof(Index));
            }
            else {
                return await Edit(model.RoleId);
            }
    }
    private void Errors(IdentityResult result) {
        foreach (var error in result.Errors) {
            ModelState.AddModelError("", error.Description);
        }
    }
}
