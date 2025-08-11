using AccountManager.Application.DTOs;
using AccountManager.Domain.Entities;
using AccountManager.Domain.Interfaces;
using AccountManager.Shared.JWT;
using Isopoh.Cryptography.Argon2;

namespace AccountManager.Application.Services
{
    public class AuthService(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator, ISessionRepository sessionRepository)
    {
        public async Task<string?> Login(LoginDto request)
        {
            try
            {
                Account? account = await userRepository.GetUserAsync(request.Email);
                if (account == null)
                    return null;

                if (!Argon2.Verify(account.Password, request.Password)) return null;

                string token = jwtTokenGenerator.GenerateToken(account.Username);

                await sessionRepository.AddSession(account, token);

                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }

        public async Task Logout(ActiveSession session)
        {
            await sessionRepository.DeletedSessionAsync(session);
        }

        public async Task<ActiveSession?> checkSession(string cookieToken)
        {
            ActiveSession? checkSession = await sessionRepository.GetSessionActiveASync(cookieToken);
            if (checkSession == null) return null;
            return checkSession;
        }
    }
}
