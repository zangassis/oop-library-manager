using System.Text.Json;

namespace LibraryManager.Models;

public class Library
{
    private List<Book> books;
    private readonly string filePath;

    public Library(string filePath)
    {
        this.filePath = filePath;
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            books = JsonSerializer.Deserialize<List<Book>>(jsonData);
        }
        else
        {
            books = new List<Book>();
        }
    }

    private void SaveData()
    {
        string jsonData = JsonSerializer.Serialize(books);
        File.WriteAllText(filePath, jsonData);
    }

    public void AddBook(Book book)
    {
        books.Add(book);
        SaveData();
    }

    public void RemoveBook(Guid bookId)
    {
        Book book = books.FirstOrDefault(b => b.Id == bookId);
        if (book != null)
        {
            books.Remove(book);
            SaveData();
        }
    }

    public IEnumerable<Book> GetBooks()
    {
        return books;
    }
}