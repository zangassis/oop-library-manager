using LibraryManager.Models;

var filePath = "book.json"; // Specify the path to JSON file

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Library>(_ => new Library(filePath));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/books", (Library library) =>
{
    var books = library.GetBooks();
    return Results.Ok(books);
});

app.MapGet("/books/{id}", (Guid id, Library library) =>
{
    var book = library.GetBooks().FirstOrDefault(b => b.Id == id);
    if (book == null)
        return Results.NotFound();
    return Results.Ok(book);
});

app.MapPost("/books", (Book book, Library library) =>
{
    library.AddBook(book);
    return Results.Created($"/books/{book.Id}", book);
});

app.MapDelete("/books/{id}", (Guid id, Library library) =>
{
    library.RemoveBook(id);
    return Results.NoContent();
});

app.Run();
