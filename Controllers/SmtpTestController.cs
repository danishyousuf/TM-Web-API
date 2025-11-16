using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TMCC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailTestController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailTestController> _logger;
        private readonly HttpClient _httpClient;

        public EmailTestController(IConfiguration configuration, ILogger<EmailTestController> logger, HttpClient httpClient)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet("SendTestEmail")]
        public async Task<IActionResult> SendTestEmail()
        {
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var receiverEmail = _configuration["EmailSettings:ReceiverEmail"];
            var clientId = _configuration["EmailSettings:ClientId"];
            var tenantId = _configuration["EmailSettings:TenantId"];
            var clientSecret = _configuration["EmailSettings:ClientSecret"];

            if (string.IsNullOrWhiteSpace(senderEmail) || string.IsNullOrWhiteSpace(receiverEmail)
                || string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(tenantId)
                || string.IsNullOrWhiteSpace(clientSecret))
            {
                return BadRequest(new { success = false, message = "EmailSettings or Client Credentials missing in configuration." });
            }

            try
            {
                // 1️⃣ Acquire Access Token dynamically
                IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
                    .Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithAuthority($"https://login.microsoftonline.com/{tenantId}")
                    .Build();

                string[] scopes = new string[] { "https://graph.microsoft.com/.default" };
                var authResult = await app.AcquireTokenForClient(scopes).ExecuteAsync();
                string accessToken = authResult.AccessToken;

                // 2️⃣ Build email payload
                var payload = new
                {
                    message = new
                    {
                        subject = "Test Email from TMCC",
                        body = new
                        {
                            contentType = "HTML",
                            content = "<p>This is a <strong>test email</strong> sent using Microsoft Graph API.</p>"
                        },
                        toRecipients = new[]
                        {
                            new { emailAddress = new { address = receiverEmail } }
                        }
                    }
                };

                var jsonPayload = JsonSerializer.Serialize(payload);

                // 3️⃣ Send email via Microsoft Graph
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
                    _logger.LogError("[EmailTestController] Failed to send test email: {Response}", responseContent);
                    return BadRequest(new { success = false, message = "Failed to send test email: " + responseContent });
                }

                _logger.LogInformation("[EmailTestController] ✅ Test email sent successfully!");
                return Ok(new { success = true, message = "Test email sent successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[EmailTestController] Error sending test email");
                return BadRequest(new { success = false, message = "Error sending test email: " + ex.Message });
            }
        }
    }
}
