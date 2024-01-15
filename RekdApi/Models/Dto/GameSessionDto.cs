public class GameSessionDto
{
  public Guid Id { get; set; }

  public string JoinCode { get; set; }
  public bool IsComplete { get; set; }
  public DateTime CreatedAt { get; set; }

  public List<string> PlayerMoves { get; set; } = new List<string>();

  public List<UserDto> Players { get; set; }
}