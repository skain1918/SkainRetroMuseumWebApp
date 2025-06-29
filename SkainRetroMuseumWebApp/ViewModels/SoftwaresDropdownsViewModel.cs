using SkainRetroMuseumWebApp.Models;

namespace SkainRetroMuseumWebApp.ViewModels; 
public class SoftwaresDropdownsViewModel {
    public IEnumerable<Platform> Platforms { get; set; }
    public SoftwaresDropdownsViewModel() {
        Platforms = new List<Platform>();
    }
}