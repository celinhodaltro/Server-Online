using System.Provider;
using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.Providers
{
    public class UserProvider : DefaultProvider
    {
        private readonly ApplicationDbContext _context;

        public UserProvider(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserInfo?> GetUserByIdAsync(int id)
        {
            return await _context.UserInfo
                                 .Where(u => !u.IsDeleted) 
                                 .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserInfo?> GetUserByUniqueIdAsync(Guid uniqueId)
        {
            return await _context.UserInfo
                                 .Where(u => !u.IsDeleted)  
                                 .FirstOrDefaultAsync(u => u.UniqueId == uniqueId);
        }

        public async Task<bool> SoftDeleteUserByIdAsync(int id)
        {
            var user = await _context.UserInfo.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null || user.IsDeleted)
            {
                return false; 
            }

            user.IsDeleted = true;
            _context.UserInfo.Update(user);
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<bool> SoftDeleteUserByUniqueIdAsync(Guid uniqueId)
        {
            var user = await _context.UserInfo.FirstOrDefaultAsync(u => u.UniqueId == uniqueId);
            if (user == null || user.IsDeleted)
            {
                return false; 
            }

            user.IsDeleted = true;
            _context.UserInfo.Update(user);
            await _context.SaveChangesAsync();
            return true; 
        }
    }
}
