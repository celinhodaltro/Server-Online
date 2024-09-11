using Microsoft.Extensions.DependencyInjection;
using Server.BusinessRules;
using System.Provider;

namespace Server.Util
{
    public static class InjecterUtil
    {

        public static void Inject(IServiceCollection Services)
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
            Services.AddScoped<PlayerBusinessRules>();

        }

        public static void InjecterProvider(IServiceCollection Services)
        {
            Services.AddScoped<DefaultProvider>();
        }
    }
}
