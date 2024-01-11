using Microsoft.EntityFrameworkCore;

namespace RekdApi.Models;

public class GameDbContext : DbContext
{
  public GameDbContext(DbContextOptions<GameDbContext> options)
      : base(options)
  {
  }

  public DbSet<User> Users { get; set; }
  public DbSet<GameSession> GameSessions { get; set; }
}