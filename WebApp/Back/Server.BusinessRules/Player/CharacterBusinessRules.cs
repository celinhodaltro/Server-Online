using Server.Entities;
using Server.Entities.Common.Characters;
using Server.Providers;
using System.Provider;

namespace Server.BusinessRules
{

    public class CharacterBusinessRules
    {

        public CharacterProvider CharacterProvider { get; set; }

        public CharacterBusinessRules(CharacterProvider CharacterProvider)
        {
            this.CharacterProvider = CharacterProvider;
        }



        #region Player

        public async Task<IEnumerable<Character>> GetAll()
        {
            var players = await CharacterProvider.GetAllAsync<Character>();
            return players;
        }

        public async Task<Character> GetById(int Id)
        {
            var player = await CharacterProvider.GetAsync<Character>(Id);
            return player;
        }

        public async Task<List<Character>> GetByUserUniqueId(Guid userUniqueId)
        {
            var Players = await CharacterProvider.GetCharacterByUserUniqueIdAsync(userUniqueId);
            return Players;
        }

        public async Task<Character?> GetPlayer(string playerName)
        {
            var Players = await CharacterProvider.GetAllAsync<Character>();

            return Players.FirstOrDefault(x => x.Name.Equals(playerName));
        }


        public async Task<Character?> GetPlayer(string accountName, string password, string charName)
        {

            var Players = await CharacterProvider.GetAllAsync<Character>();

            return Players.FirstOrDefault(x => x.Account.Email.Equals(accountName) &&
                                                    x.Account.Password.Equals(password) &&
                                                    x.Name.Equals(charName));

        }

        public async Task<Character?> GetOnlinePlayer(string accountName)
        {
            var Players = await CharacterProvider.GetAllAsync<Character>();

            return Players.FirstOrDefault(x => x.Account.Email.Equals(accountName) && x.Online);

        }

        public async Task Create(Character player)
        {
            var Character = await CharacterProvider.CreateAsync(new Character
            {
                UserId = player.UserId,
                Level = 8,
                Capacity = 470,
                Experience = 4200,
                Gender = Gender.Male,
                WorldId = 1,
                Health = 185,
                MaxHealth = 185,
                Mana = 90,
                MaxMana = 90,
                Soul = 100,
                Speed = 234,
                Name = player.Name,
                FightMode = FightMode.Balanced,
                LookType = 130,
                LookBody = 69,
                LookFeet = 95,
                LookHead = 78,
                LookLegs = 58,
                Vocation = 1,
                ChaseMode = ChaseMode.Stand,
                MaxSoul = 100,
                PlayerType = 1,
                PosX = player.PosX,
                PosY = player.PosY,
                PosZ = player.PosZ,
                TownId = player.TownId

            });

            await CharacterProvider.CreateAsync(new CharacterSkill
            {
                CharacterId = Character.Id,
                MagicLevel = 1,
                SkillAxe = 10,
                SkillDist = 10,
                SkillClub = 10,
                SkillSword = 10,
                SkillShielding = 10,
                SkillFist = 10,
                SkillFishing = 10,
            });
        }

        #endregion

        #region PlayerDepot 

        public async Task<IEnumerable<CharacterDepotItem>> GetPlayerDepotItems(uint id)
        {
            var PlayersDepotItems = await CharacterProvider.GetAllAsync<CharacterDepotItem>();
            return PlayersDepotItems.Where(x => x.CharacterId == id);
        }

        #endregion
    }
}
