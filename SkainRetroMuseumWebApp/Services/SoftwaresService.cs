using Microsoft.EntityFrameworkCore;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Models;
using SkainRetroMuseumWebApp.ViewModels;

namespace SkainRetroMuseumWebApp.Services;
public class SoftwaresService {
    private ApplicationDbContext _dbContext;
    public SoftwaresService(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<SoftwareListViewModel>> GetAllAsync() {
        var allSoftwares = await _dbContext.Softwares
            .Include(s => s.Platform)
            .OrderBy(s => s.Name)
            .ToListAsync();
        var softwareListViewModel = new List<SoftwareListViewModel>();
        foreach (var software in allSoftwares) {
            softwareListViewModel.Add(new SoftwareListViewModel() {
                Id = software.Id,
                Name = software.Name,
                Year = software.Year,
                PlatformName = software.Platform.Name,
                Description = software.Description,
            });
        }
        return softwareListViewModel;
    }
    public async Task CreateAsync(SoftwareDTO newSoftware) {
        Software software = mapToModel(newSoftware);
        await _dbContext.Softwares.AddAsync(software);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<SoftwaresDropdownsViewModel> GetPlatformsAsync() {
        var softwaresDropdownsData = new SoftwaresDropdownsViewModel() {
            Platforms = await _dbContext.Platforms.OrderBy(p => p.Name).ToListAsync(),
        };
        return softwaresDropdownsData;
    }
    public async Task UpdateAsync(int id, SoftwareDTO updatedSoftware) {
        var softwareToEdit = await _dbContext.Softwares.Include(s=>s.Platform).FirstOrDefaultAsync(s => s.Id == id);
        if (softwareToEdit != null) {
            softwareToEdit.Id = updatedSoftware.Id;
            softwareToEdit.Name = updatedSoftware.Name;
            softwareToEdit.Platform = _dbContext.Platforms.FirstOrDefault(p=>p.Id == updatedSoftware.PlatformId);
            softwareToEdit.Description = updatedSoftware.Description;
            softwareToEdit.Year = updatedSoftware.Year;
        }
        _dbContext.Update(softwareToEdit);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id) {
        var softwareToDelete = await _dbContext.Softwares.FirstOrDefaultAsync(p => p.Id == id);
        _dbContext.Remove(softwareToDelete);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<Software> GetByIdAsync(int id) {
        return await _dbContext.Softwares.Include(s => s.Platform).FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task<SoftwareDTO> GetDtoByIdAsync(int id) {
        var software = await _dbContext.Softwares.Include(s => s.Platform).FirstOrDefaultAsync(p => p.Id == id);
        if (software == null) {
            return null;
        }
        return mapToDto(software);
    }
    private SoftwareDTO mapToDto(Software software) {
        return new SoftwareDTO {
            Id = software.Id,
            Name = software.Name,
            Year = software.Year,
            PlatformId = software.Platform.Id,
            PlatformName = software.Platform.Name,
            Description = software.Description,
        };
    }
    private Software mapToModel(SoftwareDTO softwareDTO) {
        return new Software {
            Id = softwareDTO.Id,
            Name = softwareDTO.Name,
            Year = softwareDTO.Year,
            Platform = _dbContext.Platforms.FirstOrDefault(p => p.Id == softwareDTO.PlatformId),
            Description = softwareDTO.Description,
        };
    }
}