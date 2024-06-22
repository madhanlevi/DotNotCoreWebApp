using Microsoft.EntityFrameworkCore;

public class SqlDbContext(DbContextOptions<SqlDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<AuthorList> Author { get; set; }
}
