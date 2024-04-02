using Microsoft.Extensions.Logging;

namespace Coinquarry.Database.User;

public class UserRepository(PostgreSqlContext context, ILoggerFactory loggerFactory) : AbstractRepository<UserModel>(context, loggerFactory), IUserRepository
{
    
}