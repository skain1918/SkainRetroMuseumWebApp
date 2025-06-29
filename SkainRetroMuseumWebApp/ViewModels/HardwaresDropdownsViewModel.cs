using SkainRetroMuseumWebApp.Models;

namespace SkainRetroMuseumWebApp.ViewModels;

public class HardwaresDropdownsViewModel{
    public IEnumerable<Platform> Platforms { get; set; }
    public IEnumerable<Branch> Branches { get; set; }
    public HardwaresDropdownsViewModel() {
        Platforms = new List<Platform>();
        Branches = new List<Branch>();
    }
}
