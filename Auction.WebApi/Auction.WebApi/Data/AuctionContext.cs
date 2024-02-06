using Auction.WebApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auction.WebApi.Data;

public class AuctionContext(DbContextOptions<AuctionContext> options) : IdentityUserContext<User, Guid>(options)
{
    public DbSet<Lot> Lots { get; set; }

    public DbSet<Bet> Bets { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public DbSet<StaticFile> StaticFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Lot>()
            .HasOne(l => l.Author)
            .WithMany()
            .HasForeignKey(l => l.AuthorId);

        builder
            .Entity<Lot>()
            .HasMany(l => l.Tags)
            .WithMany();

        builder
            .Entity<Lot>()
            .HasMany(l => l.Images)
            .WithMany();

        builder
            .Entity<Bet>()
            .HasOne(b => b.Lot)
            .WithMany()
            .HasForeignKey(b => b.LotId);

        builder
            .Entity<Tag>()
            .HasIndex(t => t.Name)
            .IsUnique();

        builder
            .Entity<StaticFile>()
            .HasIndex(f => f.FilePath)
            .IsUnique();

        base.OnModelCreating(builder);
    }
}
