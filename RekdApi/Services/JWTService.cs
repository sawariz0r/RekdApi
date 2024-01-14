using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JWTService
{
  private readonly IConfiguration _configuration;

  public JWTService(IConfiguration configuration)
  {
    _configuration = configuration;
  }
  public string GenerateJWT(string email)
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
                new Claim(JwtRegisteredClaimNames.Sub, ""),
                new Claim(JwtRegisteredClaimNames.Email, "test@test.se"),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            }),
      Expires = DateTime.UtcNow.AddMinutes(5),
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