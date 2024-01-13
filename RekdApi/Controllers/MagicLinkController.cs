using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RekdApi.Models;

using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;
using NuGet.Protocol;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;


namespace RekdApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MagicLinkController : ControllerBase
  {
    private readonly TokenService _tokenService;
    private readonly TokenDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public MagicLinkController(TokenService tokenService, TokenDbContext dbContext, IConfiguration configuration)
    {
      _tokenService = tokenService;
      _dbContext = dbContext;
      _configuration = configuration;
    }

    [HttpPost("requestMagicLink")]
    public IActionResult RequestMagicLink([FromBody] RequestMagicLinkDto requestDto)
    {
      var token = _tokenService.GenerateToken(requestDto.Email);
      // TODO: Send an email to the user with the magic link

      // Save the token to the database
      _dbContext.Tokens.Add(new Token
      {
        Email = requestDto.Email,
        Value = token
      });

      _dbContext.SaveChanges();

      // Temporarily return the token to the user
      return Ok(token);
    }

    public class RequestMagicLinkDto
    {
      public string Email { get; set; }
    }

    [HttpGet("authenticateWithMagicLink")]
    public IActionResult AuthenticateWithMagicLink(string email, string givenToken)
    {
      // Log Email and Token
      Console.WriteLine($"Email: {email}, Token: {givenToken}");

      // Write existing tokens to the console
      var existingTokens = _dbContext.Tokens.ToList();
      Console.WriteLine($"Existing Tokens: {existingTokens.Count}");
      foreach (var existingToken in existingTokens)
      {
        Console.WriteLine($"Existing Token: {existingToken.Value}");
      }

      var isValid = _dbContext.Tokens.Any(t => t.Email == email && t.Value == givenToken);
      if (isValid)
      {


        /*var tokenToDelete = _dbContext.Tokens.First(t => t.Email == email && t.Value == token);
        _dbContext.Tokens.Remove(tokenToDelete);
        _dbContext.SaveChanges();*/

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
                new Claim(JwtRegisteredClaimNames.Sub, "test"),
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



        return Ok(stringToken);
      }
      else
      {
        return Unauthorized();
      }
    }
  }
}