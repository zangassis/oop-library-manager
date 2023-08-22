namespace LibraryManager.Models;

public class Book
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Gender { get; set; }
    public DateTime ReleaseDate { get; set; }
}