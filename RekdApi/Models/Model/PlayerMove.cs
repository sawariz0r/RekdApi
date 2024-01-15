
namespace RekdApi.Models;

public class PlayerMove
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string PlayerId { get; set; }
  public string CardId { get; set; }
  public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
