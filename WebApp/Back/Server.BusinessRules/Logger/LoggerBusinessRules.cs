using Server.Entities;
using System.Provider;

namespace Server.BusinessRules;


public class LoggerBusinessRules
{

    public DefaultProvider DefaultProvider { get; set; }

    public LoggerBusinessRules(DefaultProvider defaultProvider)
    {
        DefaultProvider = defaultProvider;
    }

    public async Task<bool> CreateLog(LogTrack Log)
    {
        try
        {
            await DefaultProvider.CreateAsync(Log);
            return true;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> DeleteLog(LogTrack log)
    {
        try
        {
            await DefaultProvider.DeleteAsync<LogTrack>(log.Id);
            return true;
        }
        catch
        {
            throw;
        }
    }
    }

