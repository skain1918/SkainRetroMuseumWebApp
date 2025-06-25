using Microsoft.EntityFrameworkCore;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Models;

namespace SkainRetroMuseumWebApp.Services;
public class PlatformsService {
    private ApplicationDbContext _dbContext;
    public PlatformsService(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
        }
    public async Task<IEnumerable<PlatformDTO>> GetAllAsync() {
        var allPlatforms = await _dbContext.Platforms
            .OrderBy(x=> x.Name)
            .ToListAsync();
        var platformDtos = new List<PlatformDTO>();
        foreach (var platform in allPlatforms) {
            platformDtos.Add(mapToDto(platform));
            }
        return platformDtos;
        }
    private PlatformDTO mapToDto(Platform platform) {
        return new PlatformDTO
            {
            Id = platform.Id,
            Name = platform.Name,
            YearSince = platform.YearSince,
            Description = platform.Description
            };
        }
    }