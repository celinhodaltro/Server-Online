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

        public async Task<Character?> GetCharacterByIdAsync(int id)
        {
            return await _context.Characters
                                 .Where(c => !c.IsDeleted)  
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Character?> GetCharacterByUniqueIdAsync(Guid uniqueId)
        {
            return await _context.Characters
                                 .Where(c => !c.IsDeleted)  
                                 .FirstOrDefaultAsync(c => c.UniqueId == uniqueId);
        }

        public async Task<bool> SoftDeleteCharacterByIdAsync(int id)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            if (character == null || character.IsDeleted)
            {
                return false; 
            }

            character.IsDeleted = true;
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<bool> SoftDeleteCharacterByUniqueIdAsync(Guid uniqueId)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.UniqueId == uniqueId);
            if (character == null || character.IsDeleted)
            {
                return false; 
            }

            character.IsDeleted = true;
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<Character>?> GetCharacterByUserUniqueIdAsync(Guid userUniqueId)
        {

            var Users = await this.GetAllAsync<UserInfo?>();
            var User = Users.FirstOrDefault(u => u.UniqueId == userUniqueId);

            if (User == null)
                throw new Exception("Error! Account not exist.");


            return await _context.Characters
                                 .Where(c => !c.IsDeleted && c.UserId == User.Id)
                                 .ToListAsync();
        }
    }
}
