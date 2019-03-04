using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using SlackAPI;

namespace Function
{
    public class FunctionHandler
    {
        public string Handle(string input) {
            FormResponse formResponse = JsonConvert.DeserializeObject<FormResponse>(input);

            var message = FormatMessage(formResponse);
            var slackSent = SendToSlack(message);

            var fnResponse = $"Response recorded from {formResponse.Email}. Slack posted? {slackSent}";

            return fnResponse;
        }

        private string FormatMessage(FormResponse response)
        {
            var msg = new StringBuilder();
            msg.Append($"{response.FirstName} {response.LastName} would like to join the Slack group!" );
            if (!string.IsNullOrWhiteSpace(response.Company))
            {
                msg.Append($"\nThey work at {response.Company}.");
            }
            if (!string.IsNullOrWhiteSpace(response.Location))
            {
                msg.Append($"\nThey say they're from {response.Location}");
            }
            msg.Append($"\nThe reason they'd like to join is to {response.JoinReason}");
            msg.Append($"\nSend them an invite link to {response.Email}");
            
            return msg.ToString();
        }

        private bool SendToSlack(string message)
        {
            var token = GetSecret("slack-token");
            var client = new SlackTaskClient(token);

            var channel = "signup";
            var response = client.PostMessageAsync(channel, message);

            return response.Result.ok;
        }

        private string GetSecret(string name)
        {
            try
            {
                using (StreamReader sr = new StreamReader("/var/openfaas/secrets/slack-token"))
                {
                    String line = sr.ReadToEnd();
                    return line;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return string.Empty;
            }
        }
    }
}