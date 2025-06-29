using SkainRetroMuseumWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SkainRetroMuseumWebApp.DTO; 
public class HardwareDTO {
    public int Id { get; set; }
    [Display(Name = "Název techniky")]
    public string Name { get; set; }
    [Display(Name = "Výrobce")]
    public string Manufacturer { get; set; }
    [Display(Name = "Typ")]
    public string Type { get; set; }
    [Display(Name = "Platforma")]
    public int PlatformId { get; set; }
    [Display(Name = "Platforma")]
    public string? PlatformName { get; set; }
    [Display(Name = "Stav")]
    public string Condition { get; set; }
    [Range(1950, 2030, ErrorMessage = "Rok musi být v rozmezí od 1950 po dnešek."), Display(Name = "Rok výroby")]
    public int? YearOfManufactured { get; set; }
    [Display(Name = "Pobočka")]
    public int BranchId { get; set; }
    public string? BranchName { get; set; }

}

