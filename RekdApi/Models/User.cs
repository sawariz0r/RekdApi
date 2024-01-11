using System.ComponentModel.DataAnnotations;

namespace RekdApi.Models;

public class User
{
  public long Id { get; set; }
  [Required]
  public string DisplayName { get; set; }
  [Required]
  public string Email { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now;

  // Expo Notification token
  public List<string> ExpoPushToken { get; set; } = new List<string>();

  // GameSessions
  public List<GameSession> GameSessions { get; set; } = new List<GameSession>();
}
