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
            // Limit token to URL safe characters (no "=")
            rng.GetBytes(tokenData);
        }

        // Prevent = in token
        string token = Base64UrlEncode(tokenData);

        // URL-safe base64 encoding without padding
        static string Base64UrlEncode(byte[] data)
        {
            string base64 = Convert.ToBase64String(data);
            return base64.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
        
        return token;
    }
}