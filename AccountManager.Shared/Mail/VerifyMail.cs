using DnsClient;
using System.ComponentModel.DataAnnotations;

namespace AccountManager.Shared.Mail
{
    public class VerifyMail
    {
        public static async Task<bool> IsExistMailAsync(string email)
        {
            if (!IsValidEmailFormat(email))
                return false;

            if (!await IsDomainValidAsync(email))
                return false;

            return true;
        }

        private static bool IsValidEmailFormat(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        private static async Task<bool> IsDomainValidAsync(string email)
        {
            var domain = email.Split('@').Last();
            var lookup = new LookupClient();
            var result = await lookup.QueryAsync(domain, QueryType.MX);
            return result.Answers.MxRecords().Any();
        }
    }
}
