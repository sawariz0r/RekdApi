using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RekdApi.Models;

public class GameDbContext : IdentityDbContext
{
  public GameDbContext(DbContextOptions<GameDbContext> options)
      : base(options)
  {
  }

  public DbSet<User> Users { get; set; }
  public DbSet<GameSession> GameSessions { get; set; }
}