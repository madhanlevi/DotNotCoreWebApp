using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public interface IBookRepository
{
    IEnumerable<AuthorList> GetBooks();
    string InsertBooks(string authorBookData);
    decimal GetTotalPrice();
}

public class BookRepository : IBookRepository
{
    private readonly SqlDbContext _context;

    public BookRepository(SqlDbContext context)
    {
        _context = context;
    }

    public IEnumerable<AuthorList> GetBooks()
    {
        try
        {
            var result = _context.Author.FromSqlRaw("EXEC GetSortedBooks").ToList();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching books: {ex.Message}");
            throw;
        }
    }

    public string InsertBooks(string authorBookData)
    {
        try
        {
            _context.Database.ExecuteSqlRaw("EXEC InsertAuthorsAndBooks @AuthorBookData", new SqlParameter("@AuthorBookData", authorBookData));
            return "Books inserted successfully.";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting books: {ex.Message}");
            throw;
        }
    }

   public decimal GetTotalPrice()
   {
    try
    {
        return _context.Books.Sum(b => b.Price);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while calculating total price: {ex.Message}");
        throw; 
    }
   }

}
