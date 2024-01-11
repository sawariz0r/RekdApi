using System.ComponentModel.DataAnnotations;

namespace RekdApi.Models;

public class GameSession
{
  public long Id { get; set; }
  public string? Name { get; set; }
  public bool IsComplete { get; set; }
  public DateTime CreatedAt { get; set; }

  // Players
  public List<User> Players { get; set; } = new List<User>();
}