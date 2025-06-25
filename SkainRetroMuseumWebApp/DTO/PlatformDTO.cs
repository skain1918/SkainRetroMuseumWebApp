using System.ComponentModel.DataAnnotations;

namespace SkainRetroMuseumWebApp.DTO
{
    public class PlatformDTO
    {

        public int Id { get; set; }
        [Display(Name = "Název platformy")]
        public string Name { get; set; }
        [Display(Name = "Rok vydání")]
        public int YearSince { get; set; }
        [Display(Name = "Krátký popis")]
        public string? Description { get; set; }

    }
}
