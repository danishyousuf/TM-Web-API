using TMCC.Models.DTO;
using System;
using System.Threading.Tasks;

namespace TMCC.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<int> RegisterUserAsync(RegisterUserDto user);
        Task<UserResponseDto> LoginAsync(string email, string passwordHash);
        Task<int> UpdateUserAsync(UpdateUserDto user);
        Task<UserResponseDto?> GetUserByEmailAsync(string email);
        Task<int> UpdatePasswordAsync(string email, string passwordHash);

        // --- New OTP Methods ---
        Task SaveOtpAsync(string email, string otp, DateTime expiry); // Save or update OTP
        Task<(string Otp, DateTime Expiry)?> GetOtpAsync(string email); // Retrieve OTP and expiry
        Task DeleteOtpAsync(string email); // Delete OTP after verification
    }
}
