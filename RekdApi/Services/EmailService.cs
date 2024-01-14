// Create a model for your email
using System.Net.Mail;

public class EmailModel
{
  public string To { get; set; }
  public string Subject { get; set; }
  public string Body { get; set; }
}

// Create an email service
public interface IEmailService
{
  Task SendEmailAsync(EmailModel email);
}

public class SmtpEmailService : IEmailService
{
  private readonly IConfiguration _configuration;

  public SmtpEmailService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public async Task SendEmailAsync(EmailModel email)
  {
    using (var client = new SmtpClient())
    {
      var smtpSettings = _configuration.GetSection("SmtpSettings");
      client.Host = smtpSettings["Host"];
      client.Port = int.Parse(smtpSettings["Port"]);
      client.EnableSsl = bool.Parse(smtpSettings["EnableSsl"]);

      // Additional configuration if needed (e.g., credentials)

      using (var message = new MailMessage("noreply@email.com", email.To)
      {
        Subject = email.Subject,
        Body = email.Body,
        IsBodyHtml = true
      })
      {
        try
        {
          await client.SendMailAsync(message);
          return;
        }
        catch (Exception ex)
        {
          var error = $"ERROR :{ex.Message}";
          // Further debug error
          Console.WriteLine(ex);
          return;
        }
      }
    }
  }
}
