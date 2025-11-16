using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TMCC.Services.IServices;

namespace TMCC.Services
{
    public class EmailOtpService : IEmailOtpService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailOtpService> _logger;
        private readonly HttpClient _httpClient;

        private readonly ISecretService _secretService;

        public EmailOtpService(
            IConfiguration configuration,
            ILogger<EmailOtpService> logger,
            HttpClient httpClient,
            ISecretService secretService)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = httpClient;
            _secretService = secretService;
        }

        public async Task SendOtpEmailAsync(string recipientEmail, string otp)
        {
            try
            {
                // 1️⃣ Read configuration
                var senderEmail = _configuration["EmailSettings:SenderEmail"];
                var clientId = await _secretService.GetClientId();
                var tenantId = await _secretService.GetTenantId();
                var clientSecret = await _secretService.GetClientSecret();

                if (string.IsNullOrWhiteSpace(senderEmail) ||
           string.IsNullOrWhiteSpace(clientId) ||
           string.IsNullOrWhiteSpace(tenantId) ||
           string.IsNullOrWhiteSpace(clientSecret))
                {
                    throw new Exception("Email settings or client credentials missing.");
                }

                // 2️⃣ Acquire access token dynamically
                IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
                    .Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithAuthority($"https://login.microsoftonline.com/{tenantId}")
                    .Build();

                string[] scopes = new string[] { "https://graph.microsoft.com/.default" };
                var authResult = await app.AcquireTokenForClient(scopes).ExecuteAsync();
                string accessToken = authResult.AccessToken;

                // 3️⃣ Build HTML email payload
                string htmlBody = $@"
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f6f8;
            color: #333;
            margin: 0;
            padding: 0;
        }}
        .email-container {{
            max-width: 600px;
            margin: 30px auto;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            overflow: hidden;
        }}
        .email-header {{
            background-color: #4a90e2;
            color: #ffffff;
            padding: 20px;
            text-align: center;
            font-size: 20px;
            font-weight: bold;
        }}
        .email-body {{
            padding: 30px 20px;
            line-height: 1.6;
        }}
        .otp-code {{
            display: inline-block;
            background-color: #f0f4ff;
            color: #4a90e2;
            font-size: 28px;
            font-weight: bold;
            letter-spacing: 4px;
            padding: 15px 25px;
            margin: 20px 0;
            border-radius: 6px;
            text-align: center;
        }}
        .email-footer {{
            padding: 20px;
            text-align: center;
            font-size: 14px;
            color: #888;
            background-color: #f4f6f8;
        }}
        .button {{
            display: inline-block;
            padding: 12px 25px;
            margin-top: 20px;
            background-color: #4a90e2;
            color: #ffffff;
            text-decoration: none;
            border-radius: 6px;
            font-weight: bold;
        }}
        @media (max-width: 600px) {{
            .email-container {{
                margin: 15px;
            }}
            .otp-code {{
                font-size: 24px;
                padding: 12px 20px;
            }}
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='email-header'>
            TM Contracting
        </div>
        <div class='email-body'>
            <p>Hi there,</p>
            <p>You recently requested to reset your password. Use the OTP below to proceed:</p>
            <div class='otp-code'>{otp}</div>
            <p>This OTP is valid for <strong>5 minutes</strong>. Please do not share it with anyone.</p>
            <p>If you did not request a password reset, please ignore this email.</p>
            <a href='#' class='button'>Contact Support</a>
        </div>
        <div class='email-footer'>
            &copy; {DateTime.Now.Year} TM Contracting. All rights reserved.
        </div>
    </div>
</body>
</html>";


                var payload = new
                {
                    message = new
                    {
                        subject = "Your OTP for Password Reset",
                        body = new
                        {
                            contentType = "HTML",
                            content = htmlBody
                        },
                        toRecipients = new[]
                        {
                            new { emailAddress = new { address = recipientEmail } }
                        }
                    }
                };

                var jsonPayload = JsonSerializer.Serialize(payload);

                // 4️⃣ Send email via Microsoft Graph
                var request = new HttpRequestMessage(HttpMethod.Post,
                    $"https://graph.microsoft.com/v1.0/users/{senderEmail}/sendMail")
                {
                    Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("[EmailOtpService] Failed to send OTP email: {Response}", responseContent);
                    throw new Exception($"OTP email sending failed: {responseContent}");
                }

                _logger.LogInformation("[EmailOtpService] OTP sent successfully to {Recipient}", recipientEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[EmailOtpService] Error sending OTP email to {Recipient}", recipientEmail);
                throw;
            }
        }
    }
}
