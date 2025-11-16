using TMCC.Db_Helper;
using TMCC.Models.DTO;
using TMCC.Repository.IRepository;
using TMCC.Services.IServices;
using System.Collections.Concurrent;

namespace TMCC.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IEmailOtpService _emailOtpService;

        public UserService(IUserRepository userRepository, IEmailOtpService emailOtpService)
        {
            _userRepository = userRepository;
            _emailOtpService = emailOtpService;
        }
        private readonly ConcurrentDictionary<string, (string Otp, DateTime Expiry)> _otpStore
    = new ConcurrentDictionary<string, (string Otp, DateTime Expiry)>();

        public async Task RegisterAsync(RegisterUserDto user)
        {
            // Hash password
            user.Password = PasswordHelper.HashPassword(user.Password);

            await _userRepository.RegisterUserAsync(user);
        }

        public async Task<UserResponseDto> LoginAsync(LoginUserDto login)
        {
            var passwordHash = PasswordHelper.HashPassword(login.Password);
            var user = await _userRepository.LoginAsync(login.Email, passwordHash);

            if (user == null)
                throw new Exception("Invalid email or password.");

            return user;
        }
        public async Task UpdateUserAsync(UpdateUserDto user)
        {
            await _userRepository.UpdateUserAsync(user);
        }
        public async Task<UserResponseDto?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }
        public async Task SendOtpAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) throw new Exception("User not found.");

            var otp = new Random().Next(100000, 999999).ToString();
            var expiry = DateTime.UtcNow.AddMinutes(5);

            // Save OTP in DB
            await _userRepository.SaveOtpAsync(email, otp, expiry);

            // Send OTP email
            await _emailOtpService.SendOtpEmailAsync(email, otp);
        }

        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            var entry = await _userRepository.GetOtpAsync(email);

            if (entry.HasValue)
            {
                var (storedOtp, expiry) = entry.Value;
                if (storedOtp == otp && expiry >= DateTime.UtcNow)
                {
                    // OTP is valid
                    return true;
                }
            }

            return false; // Invalid or expired OTP
        }


        public async Task ResetPasswordAsync(string email, string otp, string newPassword)
        {
            // 1. Verify OTP
            var isValid = await VerifyOtpAsync(email, otp);
            if (!isValid)
                throw new Exception("Invalid or expired OTP.");

            // 2. Hash the new password
            var hashed = PasswordHelper.HashPassword(newPassword);

            // 3. Update password in DB
            await _userRepository.UpdatePasswordAsync(email, hashed);

            // 4. Delete OTP from DB after successful reset
            await _userRepository.DeleteOtpAsync(email);
        }


    }
}
