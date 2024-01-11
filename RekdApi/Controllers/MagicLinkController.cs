using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RekdApi.Models;

[ApiController]
[Route("api/[controller]")]
public class MagicLinkController : ControllerBase
{
  private readonly TokenService _tokenService;
  private readonly TokenDbContext _dbContext;

  public MagicLinkController(TokenService tokenService, TokenDbContext dbContext)
  {
    _tokenService = tokenService;
    _dbContext = dbContext;
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
  public IActionResult AuthenticateWithMagicLink(string email, string token)
  {
    // Log Email and Token
    Console.WriteLine($"Email: {email}, Token: {token}");

    // Write existing tokens to the console
    var existingTokens = _dbContext.Tokens.ToList();
    Console.WriteLine($"Existing Tokens: {existingTokens.Count}");
    foreach (var existingToken in existingTokens)
    {
      Console.WriteLine($"Existing Token: {existingToken.Value}");
    }

    var isValid = _dbContext.Tokens.Any(t => t.Email == email && t.Value == token);
    if (isValid)
    {
      // TODO: Authenticate the user
      // Delete the token from the database
      var tokenToDelete = _dbContext.Tokens.First(t => t.Email == email && t.Value == token);
      _dbContext.Tokens.Remove(tokenToDelete);
      _dbContext.SaveChanges();
      
      return Ok();
    }
    else
    {
      return Unauthorized();
    }
  }
  }
