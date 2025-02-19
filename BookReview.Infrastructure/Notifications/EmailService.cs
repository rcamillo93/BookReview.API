using BookReview.Core.Services;
using Microsoft.Extensions.Configuration;
using BookReview.Core.Entity;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace BookReview.Infrastructure.Notifications
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly string _senderEmail;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = _configuration["SmtpSettings:AppKey"];
            _senderEmail = _configuration["SmtpSettings:SenderEmail"];
        }

        public async Task SendRecoveryPasswordEmailAsync(User user, string temporaryPassword, string temporaryHash)
        {
            string url = $"https://localhost/api/user/recoverpassword?hash={temporaryHash}";

            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_senderEmail, "BookReviews");
            var to = new EmailAddress(user.Email);

            string htmlContent = PopulateBody(user.FullName, temporaryPassword, url);

            var msg = MailHelper.CreateSingleEmail(from, to, "RecoveryPassword", htmlContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            Console.WriteLine($"Status: {response.StatusCode}");          
        }

        private string PopulateBody(string userName, string temporaryPassword, string url)
        {
            string body = string.Empty;
  
            string basePath = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.Parent.FullName;
   
            string templatePath = Path.Combine(basePath, "BookReview.Infrastructure", "Notifications", "Template", "RecoveryPassword.html");

            using (StreamReader reader = new StreamReader(templatePath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{{USER_NAME}}", userName);       
            body = body.Replace("{{TEMP_PASSWORD}}", temporaryPassword);
            body = body.Replace("{{RESET_LINK}}", url);

            return body;
        }
    }
}
