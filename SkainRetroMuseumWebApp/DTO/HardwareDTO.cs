using SkainRetroMuseumWebApp.Models;

namespace SkainRetroMuseumWebApp.DTO; 
public class HardwareDTO {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Type { get; set; }
    public string Condition { get; set; }
    public int? YearOfManufactured { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }

}

