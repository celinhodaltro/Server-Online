
using Microsoft.Extensions.DependencyInjection;
using Server.BusinessRules;
using System.Provider;
using Server.Providers;

namespace Server.Util
{
    public static class InjecterUtil
    {

        public static void InjectDefaultServices(this IServiceCollection Services)
        {
            InjecterDefault(Services);
            InjecterBusinessRules(Services);
            InjecterProvider(Services);
        }

        public static void InjecterDefault(IServiceCollection Services)
        {
            Services.AddScoped<ApplicationDbContext>();
        }

        public static void InjecterBusinessRules(IServiceCollection Services)
        {
            Services.AddScoped<LogBusinessRules>();
            Services.AddScoped<UserBusinessRules>();
            Services.AddScoped<CharacterBusinessRules>();

        }

        public static void InjecterProvider(IServiceCollection Services)
        {
            Services.AddScoped<DefaultProvider>();
            Services.AddScoped<UserProvider>();
            Services.AddScoped<CharacterProvider>();
        }

    }
}
