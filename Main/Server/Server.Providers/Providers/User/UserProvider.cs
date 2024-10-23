using System.Provider;
using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.Providers
{
    public class UserProvider : DefaultProvider
    {
        private readonly ApplicationDbContext? _context;

        public UserProvider(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string Name, string Password)
        {
            User? User = await _context.Set<User>()
                .Include(u => u.UserInfo)
                .Include(u => u.Characters)
                    .ThenInclude(c => c.CharacterSkills)
                .FirstOrDefaultAsync(x => x.Email.Equals(Name, StringComparison.OrdinalIgnoreCase) && x.Password.Equals(Password, StringComparison.OrdinalIgnoreCase));

            return User;


        }

        public async Task<UserInfo?> GetUserByIdAsync(int id)
        {
            return await _context.UserInfo.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<UserInfo?> GetUserByUniqueIdAsync(Guid uniqueId)
        {
            return await _context.UserInfo.FirstOrDefaultAsync(u => u.UniqueId == uniqueId && !u.IsDeleted);
        }



        public async Task<bool> SoftDeleteAsync(Guid uniqueId)
        {
            var affectedRows = await _context.UserInfo
                .Where(c => c.UniqueId == uniqueId && !c.IsDeleted)
                .ExecuteUpdateAsync(c => c.SetProperty(c => c.IsDeleted, true));

            return affectedRows > 0;
        }


        public async Task<bool> SoftDeleteAsync(int? id)
        {
            var affectedRows = await _context.UserInfo
                .Where(c => c.Id == id && !c.IsDeleted)
                .ExecuteUpdateAsync(c => c.SetProperty(c => c.IsDeleted, true));

            return affectedRows > 0;
        }
    }
}
