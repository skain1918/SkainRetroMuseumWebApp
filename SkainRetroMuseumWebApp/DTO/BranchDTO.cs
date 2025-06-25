using System.ComponentModel.DataAnnotations;

namespace SkainRetroMuseumWebApp.DTO
{
    public class BranchDTO
    {
        public int Id { get; set; }
        [Display(Name = "Název pobočky")]
        public string Name { get; set; }
        [Display(Name = "Město 🏢")]
        public string Town { get; set; }
        [Display(Name = "Adresa 📬")]
        public string Street { get; set; }
        [Display(Name = "Email 📧")]
        public string Email { get; set; }
        [Display(Name = "Telefón ☎️")]
        public int PhoneNumber { get; set; }
        [Display(Name = "Poznámka")]
        public string Note { get; set; }
    }
}
