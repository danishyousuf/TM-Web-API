using System.Threading.Tasks;

namespace TMCC.Services
{
    public interface IEmailOtpService
    {
        Task SendOtpEmailAsync(string recipientEmail, string otp);
    }
}
