namespace LibraryManager.Models;

public class Ebook : Book
{
    public string? Format { get; set; }
    public double SizeInMB { get; set; }
}