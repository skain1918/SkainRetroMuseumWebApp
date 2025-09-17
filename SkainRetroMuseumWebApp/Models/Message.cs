using System.ComponentModel.DataAnnotations;


namespace SkainRetroMuseumWebApp.Models {
    public class Message {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}
