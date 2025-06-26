using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Models;

namespace SkainRetroMuseumWebApp.Services;
public class PlatformsService
{
    private ApplicationDbContext _dbContext;
    public PlatformsService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<PlatformDTO>> GetAllAsync()
    {
        var allPlatforms = await _dbContext.Platforms
            .OrderBy(x => x.Name)
            .ToListAsync();
        var platformDtos = new List<PlatformDTO>();
        foreach (var platform in allPlatforms)
        {
            platformDtos.Add(mapToDto(platform));
        }
        return platformDtos;
    }
    public async Task CreateAsync(PlatformDTO newplatform)
    {
        await _dbContext.Platforms.AddAsync(mapToModel(newplatform));
        await _dbContext.SaveChangesAsync();
    }
    public async Task<PlatformDTO> UpdateAsync(PlatformDTO updatedplatform)
    {
        _dbContext.Update(mapToModel(updatedplatform));
        await _dbContext.SaveChangesAsync();
        return updatedplatform;
    }
    public async Task<PlatformDTO> GetByIdAsync(int id)
    {
        var platform = await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == id);
        if(platform == null)
        {
            return null;
        }
        return mapToDto(platform);
    }
    public async Task DeleteAsync(int id)
    {
        var platformToDelete = await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == id);
        _dbContext.Remove(platformToDelete);
        await _dbContext.SaveChangesAsync();
    }
    private PlatformDTO mapToDto(Platform platform)
    {
        return new PlatformDTO
        {
            Id = platform.Id,
            Name = platform.Name,
            YearSince = platform.YearSince,
            Description = platform.Description
        };
    }
    private Platform mapToModel(PlatformDTO platformDTO)
    {
        return new Platform
        {
            Id = platformDTO.Id,
            Name = platformDTO.Name,
            YearSince = platformDTO.YearSince,
            Description = platformDTO.Description,
        };
    }


}