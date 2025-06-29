using Microsoft.EntityFrameworkCore;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Models;
using SkainRetroMuseumWebApp.ViewModels;

namespace SkainRetroMuseumWebApp.Services;
public class HardwaresService {
    private ApplicationDbContext _dbContext;
    public HardwaresService(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<HardwareListViewModel>> GetAllAsync() {
        var allHardwares = await _dbContext.Hardwares
            .Include(h => h.Branch)
            .Include(h => h.Platform)
            .OrderBy(h => h.Name)
            .ToListAsync();
        var hardwareListViewModel = new List<HardwareListViewModel>();
        foreach (var hardware in allHardwares) {
            hardwareListViewModel.Add(new HardwareListViewModel() {
                Id = hardware.Id,
                Name = hardware.Name,
                Type = hardware.Type,
                Manufacturer = hardware.Manufacturer,
                PlatformName = hardware.Platform.Name,
                Condition = hardware.Condition,
                YearOfManufactured = hardware.YearOfManufactured,
                BranchName = hardware.Branch.Name,
                BranchId = hardware.Branch.Id,
            });
        }
        return hardwareListViewModel;
    }
    public async Task CreateAsync(HardwareDTO newHardware) {
            Hardware hardware = mapToModel(newHardware);
            await _dbContext.Hardwares.AddAsync(hardware);
            await _dbContext.SaveChangesAsync();
    }
    public async Task<HardwaresDropdownsViewModel> GetPlatformsAndBranchesAsync() {
        var hardwaresDropdownsData = new HardwaresDropdownsViewModel() {
            Platforms = await _dbContext.Platforms.OrderBy(p => p.Name).ToListAsync(),
            Branches = await _dbContext.Branches.OrderBy(b => b.Name).ToListAsync(),
        };
        return hardwaresDropdownsData;
    }
    public async Task UpdateAsync(int id, HardwareDTO updatedHardware) {
        var hardwareToEdit = await _dbContext.Hardwares
            .Include(h=> h.Platform)
            .Include(h => h.Branch)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (hardwareToEdit != null) {
            hardwareToEdit.Id = updatedHardware.Id;
            hardwareToEdit.Name = updatedHardware.Name;
            hardwareToEdit.Manufacturer = updatedHardware.Manufacturer;
            hardwareToEdit.Type = updatedHardware.Type;
            hardwareToEdit.Platform = _dbContext.Platforms.FirstOrDefault(p => p.Id == updatedHardware.PlatformId);
            hardwareToEdit.Condition = updatedHardware.Condition;
            hardwareToEdit.YearOfManufactured = updatedHardware.YearOfManufactured;
            hardwareToEdit.Branch = _dbContext.Branches.FirstOrDefault(b => b.Id == updatedHardware.BranchId);
        }
        _dbContext.Update(hardwareToEdit);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id) {
        var hardwareToDelete = await _dbContext.Hardwares.FirstOrDefaultAsync(h => h.Id == id);
        _dbContext.Remove(hardwareToDelete);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<Hardware> GetByIdAsync(int id) {
        return await _dbContext.Hardwares
            .Include(h => h.Platform)
            .Include(h => h.Branch)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task<HardwareDTO> GetDtoByIdAsync(int id) {
        var hardware = await _dbContext.Hardwares
            .Include(h => h.Platform)
            .Include(h => h.Branch)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (hardware == null) {
            return null;
        }
        return mapToDto(hardware);
    }
    private HardwareDTO mapToDto(Hardware hardware) {
        return new HardwareDTO {
            Id = hardware.Id,
            Name = hardware.Name,
            Manufacturer = hardware.Manufacturer,
            Type = hardware.Type,
            PlatformId = hardware.Platform.Id,
            PlatformName = hardware.Platform.Name,
            Condition = hardware.Condition,
            YearOfManufactured = hardware.YearOfManufactured,
            BranchId = hardware.Branch.Id,
            BranchName = hardware.Branch.Name,
        };
    }
    private Hardware mapToModel(HardwareDTO hardwareDTO) {
        return new Hardware {
            Id = hardwareDTO.Id,
            Name = hardwareDTO.Name,
            Manufacturer = hardwareDTO.Manufacturer,
            Type = hardwareDTO.Type,
            Platform = _dbContext.Platforms.FirstOrDefault(p => p.Id == hardwareDTO.PlatformId),
            Condition = hardwareDTO.Condition,
            YearOfManufactured = hardwareDTO.YearOfManufactured,
            Branch = _dbContext.Branches.FirstOrDefault(b => b.Id == hardwareDTO.BranchId),
        };
    }
}
