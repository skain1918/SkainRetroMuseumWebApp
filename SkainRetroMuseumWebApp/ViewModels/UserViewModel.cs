using System.ComponentModel.DataAnnotations;

namespace SkainRetroMuseumWebApp.ViewModels {
    public class UserViewModel {
        [Required, Display(Name = "Uživatelské jméno")]
        public string Username { get; set; }
        [Required, EmailAddress, Display(Name = "Email")]
        public string Email { get; set; }
        [Required, Display(Name = "Heslo")]
        public string Password { get; set; }
    }
}
