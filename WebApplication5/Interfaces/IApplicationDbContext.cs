using System.Data.Entity;
using WebApplication5.Models;

namespace WebApplication5.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Player> Players { get; set; }

        DbSet<HeadToHead> HeadToHead { get; set; }

        DbSet<Country> Countries { get; set; }
    }
}