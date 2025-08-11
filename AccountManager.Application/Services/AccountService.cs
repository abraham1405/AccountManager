using AccountManager.Domain.Entities;
using AccountManager.Domain.Interfaces;
using Isopoh.Cryptography.Argon2;

namespace AccountManager.Application.Services
{
    public class AccountService(IUserRepository userRepository)
    {
        public async Task<bool> RegisterAsync(Account request)
        {
            try
            {

                var isExist = await userRepository.GetUserAsync(request.Email);

                if (isExist != null)
                {
                    return false;
                }

                bool checkUsername = await userRepository.GetUsernameAsync(request.Username);

                if (checkUsername == true) return false;

                var passwordhash = Argon2.Hash(request.Password);
                request.Password = passwordhash;

                return await userRepository.AddAccountAsync(request);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }

        }

    }
}
