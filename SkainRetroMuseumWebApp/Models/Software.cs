using System.ComponentModel.DataAnnotations;

namespace SkainRetroMuseumWebApp.Models; 
public class Software {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public Platform Platform { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }
}

