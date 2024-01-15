

namespace RekdApi.Models;

public class Player
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public User User { get; set; }
    public List<Card> Hand { get; set; } = new List<Card>();
}