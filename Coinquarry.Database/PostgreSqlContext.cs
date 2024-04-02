using Coinquarry.Database.Tier;
using Coinquarry.Database.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Coinquarry.Database;

public class PostgreSqlContext : DbContext
{
    public IDatabaseContainer Db { get; set; }
    
    public DbSet<UserModel> User { get; set; }
    public DbSet<TierModel> Tier { get; set; }


    public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options, ILoggerFactory loggerFactory) : base(options)
    {
        Db = new DatabaseContainer(this, loggerFactory);
    }
}