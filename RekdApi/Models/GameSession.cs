using System.ComponentModel.DataAnnotations;

namespace RekdApi.Models;

public class GameSession
{
  public long Id { get; set; }
  public string JoinCode { get; set; } = new JoinCodeService().GenerateJoinCode();
  public bool IsComplete { get; set; }
  public DateTime CreatedAt { get; set; }

  // Players
  public List<User> Players { get; set; } = new List<User>();
}