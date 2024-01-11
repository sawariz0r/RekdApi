using System;
using System.Security.Cryptography;
using System.Text;

public class TokenService
{
    public string GenerateToken(string email)
    {
        byte[] tokenData = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(tokenData);
        }
        string token = Convert.ToBase64String(tokenData);

        return token;
    }
}