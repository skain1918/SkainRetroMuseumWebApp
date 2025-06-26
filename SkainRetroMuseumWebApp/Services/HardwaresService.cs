using Microsoft.EntityFrameworkCore;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Models;

namespace SkainRetroMuseumWebApp.Services;

public class HardwaresService
{
    private ApplicationDbContext _dbContext;
    public HardwaresService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<HardwareDTO>> GetAllAsync()
    {
        var allHardwares = await _dbContext.Hardwares
            .Include(h => h.Branch)
            .OrderBy(h => h.Name)
            .ToListAsync();
        var hardwareDtos = new List<HardwareDTO>();
        foreach (var hardware in allHardwares)
        {
            hardwareDtos.Add(mapToDto(hardware));
        }
        return hardwareDtos;
    }
    public async Task CreateAsync(HardwareDTO newHardware)
    {
        var hardware = mapToModel(newHardware);
        await _dbContext.Hardwares.AddAsync(hardware);
        await _dbContext.SaveChangesAsync();
    }
    private HardwareDTO mapToDto(Hardware hardware)
    {
        return new HardwareDTO
        {
            Id = hardware.Id,
            Name = hardware.Name,
            Manufacturer = hardware.Manufacturer,
            Type = hardware.Type,
            Condition = hardware.Condition,
            YearOfManufactured = hardware.YearOfManufactured,
            BranchId = hardware.Branch.Id,
            BranchName = hardware.Branch?.Name
        };
    }
    private Hardware mapToModel(HardwareDTO hardwareDTO)
    {
        return new Hardware
        {
            Id = hardwareDTO.Id,
            Name = hardwareDTO.Name,
            Manufacturer = hardwareDTO.Manufacturer,
            Type = hardwareDTO.Type,
            Condition = hardwareDTO.Condition,
            YearOfManufactured = hardwareDTO.YearOfManufactured,
            //BranchId = hardwareDTO.BranchId
        };
    }
}
