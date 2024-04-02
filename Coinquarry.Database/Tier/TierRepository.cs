using Microsoft.Extensions.Logging;

namespace Coinquarry.Database.Tier;

public class TierRepository(PostgreSqlContext context, ILoggerFactory loggerFactory) : AbstractRepository<TierModel>(context, loggerFactory), ITierRepository
{
    
}