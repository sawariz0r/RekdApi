using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RekdApi.Models;

public class GameSession
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.None)] // Important: Disable auto-generation
  public Guid Id { get; set; } = Guid.NewGuid();
  public string? JoinCode { get; set; } = new JoinCodeService().GenerateJoinCode();
  public bool IsComplete { get; set; } = false;
  public bool HasStarted { get; set; } = false;
  public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

  public List<PlayerMove> PlayerMoves { get; set; } = new List<PlayerMove>();

  // Players
  public List<Player> Players { get; set; } = new List<Player>();

  // Method to set hand for all players
  public void SetStarterHands()
  {
    var cardLibrary = new CardLibrary();
    var initialCards = cardLibrary.Cards.Take(10).ToList();
    foreach (var player in Players)
    {
      player.Hand.AddRange(initialCards);
    }
  }

  public void StartGame()
  {
    HasStarted = true;
    JoinCode = null;
    SetStarterHands();
  }

  public void AddMove(string playerId, string cardId)
  {
    PlayerMoves.Add(new PlayerMove
    {
      PlayerId = playerId,
      CardId = cardId
    });
  }
}