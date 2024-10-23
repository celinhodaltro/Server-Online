using System.Provider;
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

        public async Task<bool> SoftDeleteCharacterByIdAsync(int? id)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            if (character == null || character.IsDeleted.Value)
            {
                return false; 
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


        public async Task<List<Character>?> GetCharacterByUserUniqueIdAsync(Guid userUniqueId)
        {

            var Users = await this.GetAllAsync<UserInfo?>();
            var User = Users.FirstOrDefault(u => u.UniqueId == userUniqueId);

            if (User == null)
                return new List<Character>();


            return await _context.Characters
                                 .Where(c => !c.IsDeleted.Value && c.UserId == User.Id)
                                 .ToListAsync();
        }
    }
}
