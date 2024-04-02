using Coinquarry.Database.Tier;
using Coinquarry.Database.User;
using Microsoft.Extensions.Logging;

namespace Coinquarry.Database;

public class DatabaseContainer : IDatabaseContainer
{
    public IUserRepository UserRepository { get; }
    public ITierRepository TierRepository { get; }

    
    public DatabaseContainer(PostgreSqlContext context, ILoggerFactory loggerFactory)
    {
        UserRepository = new UserRepository(context, loggerFactory);
        TierRepository = new TierRepository(context, loggerFactory);
    }
}