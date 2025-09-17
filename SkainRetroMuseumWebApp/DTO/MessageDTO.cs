using System.ComponentModel.DataAnnotations;

namespace SkainRetroMuseumWebApp.DTO {
    public class MessageDTO {
        public int Id { get; set; }
        [Display(Name = "Jméno navštěvníka")]
        public string Name { get; set; }
        public string Email { get; set; }
        [Display(Name = "Vzkaz")]
        public string Content { get; set; }
        [Display(Name = "Datum a čas")]
        public DateTime? SentAt { get; set; }
    }
}
