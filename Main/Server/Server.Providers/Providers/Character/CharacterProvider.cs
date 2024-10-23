using System.Provider;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.Providers
{
    public class CharacterProvider : DefaultProvider
    {
        private readonly ApplicationDbContext _context;

        public CharacterProvider(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Character?> GetAsync(int? id)
        {
            return await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<Character?> GetAsync(string Name)
        {
            return await _context.Characters.FirstOrDefaultAsync(c => c.Name == Name && !c.IsDeleted);
        }

        public async Task<Character?> GetAsync(Guid uniqueId)
        {
            return await _context.Characters.FirstOrDefaultAsync(c => c.UniqueId == uniqueId && !c.IsDeleted);
        }

        public async Task<bool> SoftDeleteAsync(int? id)
        {
            var affectedRows = await _context.Characters
                .Where(c => c.Id == id && !c.IsDeleted)
                .ExecuteUpdateAsync(c => c.SetProperty(c => c.IsDeleted, true));

            return affectedRows > 0;
        }

        public async Task<bool> SoftDeleteAsync(Guid uniqueId)
        {
            var affectedRows = await _context.Characters
                .Where(c => c.UniqueId == uniqueId && !c.IsDeleted)
                .ExecuteUpdateAsync(c => c.SetProperty(c => c.IsDeleted, true));

            return affectedRows > 0;
        }


        public async Task<List<Character>> GetByUserUniqueIdAsync(Guid userUniqueId)
        {
            var User = await _context.Set<User>().Include(u => u.Characters)
                                                  .FirstOrDefaultAsync(c => c.UniqueId == userUniqueId);

            if (User is {Characters: null})
                return new();

            return User.Characters.ToList();
        }
    }
}
