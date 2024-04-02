using Coinquarry.Database.Tier;
using Coinquarry.Database.User;

namespace Coinquarry.Database;

public interface IDatabaseContainer
{
    IUserRepository UserRepository { get; }
    ITierRepository TierRepository { get; }
}