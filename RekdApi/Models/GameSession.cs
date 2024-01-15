using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RekdApi.Models;

public class GameSession
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  public string JoinCode { get; set; } = new JoinCodeService().GenerateJoinCode();
  public bool IsComplete { get; set; } = false;
  public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

  public List<string> PlayerMoves { get; set; } = new List<string>();

  // Players
  public List<User> Players { get; set; } = new List<User>();
}