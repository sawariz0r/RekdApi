using Microsoft.EntityFrameworkCore;

namespace RekdApi.Models;

public class TokenDbContext : DbContext
{
  public TokenDbContext(DbContextOptions<TokenDbContext> options) : base(options)
  {
  }

  public DbSet<Token> Tokens { get; set; }
}
