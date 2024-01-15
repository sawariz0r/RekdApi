using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RekdApi.Models;

public class User: IdentityUser
{
  public string DisplayName { get; set; }
  [Required]
  [EmailAddress]
  public override string Email { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

  // Expo Notification tokeny
  public List<string> ExpoPushToken { get; set; } = new List<string>();

}
