using Server.BusinessRules;
using System.Provider;

namespace API.Injecter
{
    public static class ConfigInjecter
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

        }

        public static void InjecterProvider(IServiceCollection Services)
        {
            Services.AddScoped<DefaultProvider>();
        }
    }
}
