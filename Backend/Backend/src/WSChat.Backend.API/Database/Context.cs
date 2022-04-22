using Microsoft.EntityFrameworkCore;

namespace Polichat_Backend.Database;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> builder) : base(builder)
    {
    }
    
    public DbSet<Question> Questions => Set<Question>();
}