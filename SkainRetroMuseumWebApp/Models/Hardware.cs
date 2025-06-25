using System.ComponentModel.DataAnnotations;

namespace SkainRetroMuseumWebApp.Models {
    public class Hardware {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public string Condition { get; set; }
        public int? YearOfManufactured { get; set; }

        }
    }
