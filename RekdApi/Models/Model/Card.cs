namespace RekdApi.Models;

public enum CardType
{
  HasBlank,
  IsCounter,
  IsReverse,
  IsCustom
}

public class Card
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public string Name { get; set; }
  public string Description { get; set; }
  public string Category { get; set; }

  public CardType Type { get; set; }
}