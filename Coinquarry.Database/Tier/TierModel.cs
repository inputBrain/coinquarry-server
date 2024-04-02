using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Coinquarry.Database.User;

namespace Coinquarry.Database.Tier;

public class TierModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public UserModel User { get; set; }

    [Range(0, 9)]
    public int Tier { get; set; }
    
    public DateTimeOffset? StartedAt { get; set; }
    
    public DateTimeOffset? EndedAt { get; set; }


    public static TierModel CreateEmpty()
    {
        return new TierModel
        {
            Tier = 0,
        };
    }



    public void UpdateTier(int tier, DateTimeOffset startedAt, DateTimeOffset endedAt)
    {
        Tier = tier;
        StartedAt = startedAt;
        EndedAt = endedAt;
    }
}