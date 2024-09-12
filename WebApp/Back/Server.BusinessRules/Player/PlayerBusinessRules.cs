using Game.Common.Creatures.Players;
using Server.Entities;
using System.Provider;

namespace Server.BusinessRules
{

    public class PlayerBusinessRules
    {

        public DefaultProvider DefaultProvider { get; set; }

        public PlayerBusinessRules(DefaultProvider defaultProvider)
        {
            DefaultProvider = defaultProvider;
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            var players = await DefaultProvider.GetAllAsync<Player>();
            return players;
        }

        public async Task<Player> GetById(int Id)
        {
            var player = await DefaultProvider.GetAsync<Player>(Id);
            return player;
        }


        #region Player
        public async Task<Player?> GetPlayer(string playerName)
        {
            var Players = await DefaultProvider.GetAllAsync<Player>();

            return Players.FirstOrDefault(x => x.Name.Equals(playerName));
        }


        public async Task<Player?> GetPlayer(string accountName, string password, string charName)
        {

            var Players = await DefaultProvider.GetAllAsync<Player>();

            return Players.FirstOrDefault(x => x.Account.Email.Equals(accountName) &&
                                                    x.Account.Password.Equals(password) &&
                                                    x.Name.Equals(charName));

        }

        public async Task<Player?> GetOnlinePlayer(string accountName)
        {
            var Players = await DefaultProvider.GetAllAsync<Player>();

            return Players.FirstOrDefault(x => x.Account.Email.Equals(accountName) && x.Online);

        }

        public async Task Create(Player player)
        {
            await DefaultProvider.CreateAsync(new Player
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
                MagicLevel = 1,
                Vocation = 1,
                ChaseMode = ChaseMode.Stand,
                MaxSoul = 100,
                PlayerType = 1,
                PosX = player.PosX,
                PosY = player.PosY,
                PosZ = player.PosZ,
                SkillAxe = 10,
                SkillDist = 10,
                SkillClub = 10,
                SkillSword = 10,
                SkillShielding = 10,
                SkillFist = 10,
                TownId = player.TownId,
                SkillFishing = 10
            });
        }

        #endregion

        #region PlayerDepot 

        public async Task<IEnumerable<PlayerDepotItem>> GetPlayerDepotItems(uint id)
        {
            var PlayersDepotItems = await DefaultProvider.GetAllAsync<PlayerDepotItem>();
            return PlayersDepotItems.Where(x => x.PlayerId == id);
        }

        #endregion
    }
}
