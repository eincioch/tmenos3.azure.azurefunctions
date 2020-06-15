using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace TMenos3.Azure.AzureFunctions.Demos.EmailSender
{
    public static class EmailSenderQueue
    {
        [FunctionName("EmailSenderQueue")]
        public static void Run(
            [QueueTrigger("emails", Connection = "StorageConnectionString")]
                Email email,
            [SendGrid(ApiKey = "SendGridApiKey")]
                out SendGridMessage message,
            ILogger logger)
        {
            message = new SendGridMessage();
            message.AddTo(email.To);
            message.AddContent("text/html", email.Body);
            message.SetFrom(new EmailAddress("azure@tmenos3.com"));
            message.SetSubject(email.Subject);
        }
    }
}
