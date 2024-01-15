using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RekdApi.Models;

public class JWTService
{
  private readonly IConfiguration _configuration;

  public JWTService(IConfiguration configuration)
  {
    _configuration = configuration;
  }
  public string GenerateJWT(User user)
  {
    // Create a JWT 
    var issuer = _configuration.GetValue<string>("Jwt:Issuer");
    var audience = _configuration.GetValue<string>("Jwt:Audience");
    //        var key = Encoding.ASCII.GetBytes
    var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[]
        {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            }),
      Expires = DateTime.UtcNow.AddMonths(3),
      Issuer = issuer,
      Audience = audience,
      SigningCredentials = new SigningCredentials
        (new SymmetricSecurityKey(key),
        SecurityAlgorithms.HmacSha512Signature)
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    var jwtToken = tokenHandler.WriteToken(token);
    var stringToken = tokenHandler.WriteToken(token);

    return stringToken;
  }
}