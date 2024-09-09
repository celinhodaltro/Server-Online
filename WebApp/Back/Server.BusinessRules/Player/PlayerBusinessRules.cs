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

        public async Task<IEnumerable<PlayerResponseViewModel>> GetAll()
        {
            var players = await _playerRepository.GetAllAsync();
            var response = Mapper.Map<IEnumerable<PlayerResponseViewModel>>(players);
            return response;
        }

        public async Task<PlayerResponseViewModel> GetById(int playerId)
        {
            var player = await _playerRepository.GetAsync(playerId);
            var response = Mapper.Map<PlayerResponseViewModel>(player);
            return response;
        }

        public async Task Create(PlayerPostRequest playerPostRequest)
        {
            await _playerRepository.Add(new PlayerEntity
            {
                AccountId = playerPostRequest.AccountId,
                Level = 8,
                Capacity = 470,
                Experience = 4200,
                Gender = (Gender)playerPostRequest.Sex,
                WorldId = 1,
                Health = 185,
                MaxHealth = 185,
                Mana = 90,
                MaxMana = 90,
                Soul = 100,
                Speed = 234,
                Name = playerPostRequest.Name,
                FightMode = FightMode.Balanced,
                LookType = 130,
                LookBody = 69,
                LookFeet = 95,
                LookHead = 78,
                LookLegs = 58,
                MagicLevel = 1,
                Vocation = (byte)playerPostRequest.Vocation,
                ChaseMode = ChaseMode.Stand,
                MaxSoul = 100,
                PlayerType = 1,
                PosX = playerPostRequest.PosX,
                PosY = playerPostRequest.PosY,
                PosZ = playerPostRequest.PosZ,
                SkillAxe = 10,
                SkillDist = 10,
                SkillClub = 10,
                SkillSword = 10,
                SkillShielding = 10,
                SkillFist = 10,
                TownId = playerPostRequest.Town,
                SkillFishing = 10
            });
        }
    }
}
