using System.ComponentModel.DataAnnotations;

namespace SkainRetroMuseumWebApp.Models {
    public class Platform {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int YearSince { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        }
    }
