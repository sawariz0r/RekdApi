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
using Microsoft.AspNetCore.Identity;


namespace RekdApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MagicLinkController : ControllerBase
  {
    private readonly TokenService _tokenService;
    private readonly TokenDbContext _dbContext;

    private readonly GameDbContext _gameDbContext;
    private readonly IConfiguration _configuration;

    private readonly UserManager<User> _userManager;


    public MagicLinkController(TokenService tokenService, TokenDbContext dbContext, GameDbContext gameDbContext, IConfiguration configuration, UserManager<User> userManager)
    {
      _tokenService = tokenService;
      _dbContext = dbContext;
      _gameDbContext = gameDbContext;
      _configuration = configuration;
      _userManager = userManager;
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

      IEmailService emailService = new SmtpEmailService(_configuration);

      var email = requestDto.Email;
      var subject = "Magic Link";
      var link = $"https://localhost:7027/api/MagicLink/authenticateWithMagicLink?email={email}&givenToken={token}";
      var body = $"Your magic link is: <a href='{link}'>{link}</a>";
      var emailModel = new EmailModel
      {
        To = email,
        Subject = subject,
        Body = body
      };

      emailService.SendEmailAsync(emailModel);

      // Temporarily return the token to the user
      return Ok(token);
    }

    public class RequestMagicLinkDto
    {
      public string Email { get; set; }
    }

    [HttpGet("authenticateWithMagicLink")]
    async public Task<IActionResult> AuthenticateWithMagicLink(string email, string givenToken)
    {
      // Log Email and Token
      Console.WriteLine($"Email: {email}, Token: {givenToken}");

      var isValid = _dbContext.Tokens.Any(t => t.Email == email && t.Value == givenToken);
      if (isValid)
      {
        var existingUser = await _userManager.FindByNameAsync(email);

        if (existingUser == null)
        {
          var newUser = new User
          {
            UserName = email,
            Email = email
            // Add other properties as needed
          };
          // Create the user
          var result = await _userManager.CreateAsync(newUser, "YourPasswordHere1!");
          Console.WriteLine(result);

          existingUser = await _userManager.FindByNameAsync(email);
          if (!result.Succeeded)
          {
            return BadRequest();
          }
        }
        if (existingUser != null)
        {
          var tokenToDelete = _dbContext.Tokens.First(t => t.Email == email && t.Value == givenToken);
          _dbContext.Tokens.Remove(tokenToDelete);
          _dbContext.SaveChanges();

          JWTService jwtService = new JWTService(_configuration);
          var stringToken = jwtService.GenerateJWT(existingUser);

          return Ok(stringToken);
        } else {
          return BadRequest();
        }
      }
      else
      {
        return Unauthorized();
      }
    }
  }
}