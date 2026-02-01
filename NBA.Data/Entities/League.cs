
namespace NBA.Data.Entities;

public partial class League
{
    public long Leagueid { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Tscreated { get; set; }

    public DateTime? Tsupdated { get; set; }

    public string Usercreated { get; set; } = null!;

    public string? Userupdated { get; set; }

    public virtual ICollection<Leagueteam> Leagueteams { get; set; } = new List<Leagueteam>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
