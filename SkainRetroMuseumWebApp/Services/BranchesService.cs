using Microsoft.EntityFrameworkCore;
using SkainRetroMuseumWebApp.DTO;
using SkainRetroMuseumWebApp.Models;

namespace SkainRetroMuseumWebApp.Services;

public class BranchesService
{
    private ApplicationDbContext _dbContext;
    public BranchesService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<BranchDTO>> GetAllAsync()
    {
        var allBranches = await _dbContext.Branches
            .OrderBy(x => x.Name)
            .ToListAsync();
        var branchDtos = new List<BranchDTO>();
        foreach (var branch in allBranches)
        {
            branchDtos.Add(mapToDto(branch));
        }
        return branchDtos;
    }
    public async Task CreateAsync(BranchDTO newBranch)
    {
        await _dbContext.Branches.AddAsync(mapToModel(newBranch));
        await _dbContext.SaveChangesAsync();
    }
    public async Task<BranchDTO> UpdateAsync(BranchDTO updatedBranch)
    {
        _dbContext.Update(mapToModel(updatedBranch));
        await _dbContext.SaveChangesAsync();
        return updatedBranch;
    }
    public async Task<BranchDTO> GetByIdAsync(int id)
    {
        var branch = await _dbContext.Branches.FirstOrDefaultAsync(p => p.Id == id);
        if (branch == null)
        {
            return null;
        }
        return mapToDto(branch);
    }
    public async Task DeleteAsync(int id)
    {
        var branchToDelete = await _dbContext.Branches.FirstOrDefaultAsync(p => p.Id == id);
        _dbContext.Remove(branchToDelete);
        await _dbContext.SaveChangesAsync();
    }
    private BranchDTO mapToDto(Branch branch)
    {
        return new BranchDTO
        {
            Id = branch.Id,
            Name = branch.Name,
            Town = branch.Town,
            Street = branch.Street,
            Email = branch.Email,
            PhoneNumber = branch.PhoneNumber,
            Note = branch.Note,
        };
    }
    private Branch mapToModel(BranchDTO branchDTO)
    {
        return new Branch
        {
            Id = branchDTO.Id,
            Name = branchDTO.Name,
            Town = branchDTO.Town,
            Street = branchDTO.Street,
            Email = branchDTO.Email,
            PhoneNumber = branchDTO.PhoneNumber,
            Note = branchDTO.Note,
        };
    }
}
