using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

/// <summary>
/// API controller for managing books.
/// </summary>
[Route("api/books/")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    /// <summary>
    /// Constructor for BookController.
    /// </summary>
    /// <param name="bookRepository">The book repository implementation.</param>
    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    /// <summary>
    /// Retrieves a list of books sorted by Publisher, Author Last Name, Author First Name, and Title.
    /// </summary>
    /// <returns>A sorted list of books.</returns>
    [HttpGet("sorted-books")]
    public ActionResult<IEnumerable<AuthorList>> GetSortedBooks()
    {
        try
        {
            var sortedBooks = _bookRepository.GetBooks();
            return Ok(sortedBooks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a list of books sorted by Author Last Name, Author First Name, and Title.
    /// </summary>
    /// <returns>A sorted list of books.</returns>
    [HttpGet("sorted-author-books")]
    public ActionResult GetAuthorsSortedBooks()
    {
        try
        {
            var sortedBooks = _bookRepository.GetBooks()
                .Select(a => new AuthorList 
                {
                    Author = a.Author,
                    Title = a.Title
                });

            return Ok(sortedBooks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Saves a collection of books in bulk.
    /// </summary>
    /// <param name="books">The collection of books to save.</param>
    /// <returns>An IActionResult indicating success or failure.</returns>
    [HttpPost("insert-books")]
    public ActionResult SaveBulkBooks(List<Book> books)
    {
        try
        {
            string result = _bookRepository.InsertBooks(JsonSerializer.Serialize(books));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves the total price of all books.
    /// </summary>
    /// <returns>The total price of all books.</returns>
    [HttpGet("total-price")]
    public ActionResult GetTotalPrice()
    {
        try
        {
            var res = _bookRepository.GetTotalPrice();
            return Ok(new { totalPrice = res });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
