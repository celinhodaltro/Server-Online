using Game.World.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Server.BusinessRules;
using System.Provider;
using Front.Services;

namespace Server.Util
{
    public static class InjecterUtil
    {

        public static void InjectDefaultServices(this IServiceCollection Services)
        {
            InjecterDefault(Services);
            InjecterBusinessRules(Services);
            InjecterProvider(Services);
            InjecterService(Services);
        }

        public static void InjecterDefault(IServiceCollection Services)
        {
            Services.AddScoped<ApplicationDbContext>();
        }

        public static void InjecterBusinessRules(IServiceCollection Services)
        {
            Services.AddScoped<LogBusinessRules>();
            Services.AddScoped<UserBusinessRules>();
            Services.AddScoped<PlayerBusinessRules>();

        }

        public static void InjecterProvider(IServiceCollection Services)
        {
            Services.AddScoped<DefaultProvider>();
        }

        public static void InjecterService (IServiceCollection Services)
        {
            Services.AddScoped<AuthenticationStateProvider, AuthService>();
            Services.AddScoped<AuthService>();
            Services.AddScoped<ApiService>();
            Services.AddScoped<CharacterService>();
        }
    }
}
