using SkainRetroMuseumWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SkainRetroMuseumWebApp.DTO {
    public class SoftwareDTO {
        public int Id { get; set; }
        [Display(Name = "Název")]
        public string Name { get; set; }
        [Range(1950, 2030, ErrorMessage ="Rok musi být v rozmezí od 1950 po dnešek."), Display(Name = "Rok vydání")]
        public int Year { get; set; }
        [Display(Name = "Platforma")]
        public int PlatformId { get; set; }
        public string? PlatformName { get; set; }
        [Display(Name = "Podrobnosti")]
        public string? Description { get; set; }
    }
}
