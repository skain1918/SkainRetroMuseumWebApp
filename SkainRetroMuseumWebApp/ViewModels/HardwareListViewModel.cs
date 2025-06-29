using System.ComponentModel.DataAnnotations;

namespace SkainRetroMuseumWebApp.ViewModels {
    public class HardwareListViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string PlatformName { get; set; }
        public string Condition { get; set; }
        public int? YearOfManufactured { get; set; }
        public string BranchName { get; set; }
        public int BranchId { get; set; }
    }
}
