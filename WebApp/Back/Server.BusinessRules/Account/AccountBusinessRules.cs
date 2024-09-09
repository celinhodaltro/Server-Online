using Server.Entities;
using System.Provider;

namespace Server.BusinessRules
{

    public class AccountBusinessRules
    {

        public DefaultProvider DefaultProvider { get; set; }

        public AccountBusinessRules(DefaultProvider defaultProvider)
        {
            DefaultProvider = defaultProvider;
        }

        public async Task Create(AccountPostRequest request)
        {
            await _accountRepository.Insert(new AccountEntity
            {
                Password = request.Password,
                CreatedAt = DateTime.UtcNow,
                EmailAddress = request.Email,
                PremiumTime = request.PremiumDays,
                AllowManyOnline = false
            });
        }
    }
}
