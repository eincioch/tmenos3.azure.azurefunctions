using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using System.IO;

namespace TMenos3.Azure.AzureFunctions.Demos.EmailSender
{
    public static partial class EmailSenderHttp
    {
        [FunctionName("EmailSenderHttp")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "emails")]
                HttpRequest req,
            [SendGrid(ApiKey = "SendGridApiKey")]
                out SendGridMessage message,
            ILogger logger)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var email = JsonConvert.DeserializeObject<Email>(requestBody);

            message = new SendGridMessage();
            message.AddTo(email.To);
            message.AddContent("text/html", email.Body);
            message.SetFrom(new EmailAddress("azure@tmenos3.com"));
            message.SetSubject(email.Subject);

            return new OkObjectResult("Email sent...");
        }
    }
}
