using System.ComponentModel.DataAnnotations;

namespace SkainRetroMuseumWebApp.ViewModels {
    public class LoginViewModel {
        [Required, Display(Name = "Uživatelské jméno")]
        public string Username { get; set; }
        [Required, Display(Name = "Heslo")]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
