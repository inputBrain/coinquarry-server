using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Coinquarry.Database.Tier;

namespace Coinquarry.Database.User;

public class UserModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string WalletId { get; set; }

    public double BalanceOnWebsite { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public TierModel Tier { get; set; }


    public static UserModel CreateModel(string walletId)
    {
        return new UserModel
        {
            WalletId = walletId,
            BalanceOnWebsite = 0.0,
            Tier = TierModel.CreateEmpty()
        };
    }
}