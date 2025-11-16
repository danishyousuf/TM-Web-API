using TMCC.Models.DTO;

namespace TMCC.Services.IServices
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserDto user);
        Task<UserResponseDto> LoginAsync(LoginUserDto login);
        Task UpdateUserAsync(UpdateUserDto user);
        Task<UserResponseDto?> GetUserByEmailAsync(string email);
        Task SendOtpAsync(string email);
        Task<bool> VerifyOtpAsync(string email, string otp);
        Task ResetPasswordAsync(string email, string otp, string newPassword);
    }


}
