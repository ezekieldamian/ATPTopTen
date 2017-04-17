using System.Data.Entity;
using ATPTopTen.Models;

namespace ATPTopTen.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Player> Players { get; set; }

        DbSet<HeadToHead> HeadToHead { get; set; }

        DbSet<Country> Countries { get; set; }
    }
}