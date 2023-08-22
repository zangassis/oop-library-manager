using System.Text.Json;

namespace LibraryManager.Models;

public class Library
{
    private List<Book> books;
    private readonly string libraryFilePath;

    public Library(string libraryFilePath)
    {
        this.libraryFilePath = libraryFilePath;
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(libraryFilePath))
        {
            string jsonData = File.ReadAllText(libraryFilePath);
            books = JsonSerializer.Deserialize<List<Book>>(jsonData);
        }
        else
            books = new List<Book>();
    }

    private void SaveData()
    {
        string jsonData = JsonSerializer.Serialize(books);
        File.WriteAllText(libraryFilePath, jsonData);
    }

    public void AddBook(Book book)
    {
        if (book is EBook ebook)
            books.Add(ebook);
        else
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

    public IEnumerable<EBook> GetEbooks()
    {
        return books.OfType<EBook>();
    }
}