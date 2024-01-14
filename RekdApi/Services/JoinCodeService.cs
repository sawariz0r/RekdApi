using System;
using System.Security.Cryptography;
using System.Text;

public class JoinCodeService
{
  private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
  private const int CodeLength = 5;

  private readonly RandomNumberGenerator random;

  public JoinCodeService()
  {
    random = RandomNumberGenerator.Create();
  }

  public string GenerateJoinCode()
  {
    byte[] data = new byte[CodeLength];
    random.GetBytes(data);

    StringBuilder joinCode = new StringBuilder(CodeLength);

    foreach (byte b in data)
    {
      joinCode.Append(Characters[b % Characters.Length]);
    }

    return joinCode.ToString();
  }
}