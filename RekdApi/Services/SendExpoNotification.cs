using Expo.Server.Client;
using Expo.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class SendExpoNotification
{
  public async Task SendNotification(string token)
  {
    var expoSDKClient = new PushApiClient();
    var pushTicketReq = new PushTicketRequest()
    {
      PushTo = new List<string>() { token },
      PushBadgeCount = 7,
      PushBody = "Test Push - Msg"
    };
    var result = await expoSDKClient.PushSendAsync(pushTicketReq);

    if (result?.PushTicketErrors?.Count() > 0)
    {
      foreach (var error in result.PushTicketErrors)
      {
        Console.WriteLine($"Error: {error.ErrorCode} - {error.ErrorMessage}");
      }
    }
  }
}