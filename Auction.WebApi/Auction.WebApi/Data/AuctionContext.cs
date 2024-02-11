using Auction.WebApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auction.WebApi.Data;

public class AuctionContext(DbContextOptions<AuctionContext> options) : IdentityUserContext<User, Guid>(options)
{
    public DbSet<Lot> Lots { get; set; }

    public DbSet<Bet> Bets { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Message> Messages { get; set; }

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
            .WithMany(l => l.Bets)
            .HasForeignKey(b => b.LotId);

        builder
            .Entity<Tag>()
            .HasIndex(t => t.Name)
            .IsUnique();

        builder
            .Entity<StaticFile>()
            .HasIndex(f => f.FilePath)
            .IsUnique();

        builder
            .Entity<Message>()
            .HasOne(m => m.Lot)
            .WithMany()
            .HasForeignKey(m => m.LotId);

        builder
            .Entity<Message>()
            .HasOne(m => m.Author)
            .WithMany()
            .HasForeignKey(m => m.AuthorId);

        AutoGenerateCreatedAtValue<Bet>(builder);
        AutoGenerateCreatedAtValue<Lot>(builder);
        AutoGenerateCreatedAtValue<Tag>(builder);
        AutoGenerateCreatedAtValue<StaticFile>(builder);
        AutoGenerateCreatedAtValue<Message>(builder);

        base.OnModelCreating(builder);
    }

    private void AutoGenerateCreatedAtValue<TEntity>(ModelBuilder builder) where TEntity : BaseEntity
    {
        builder
            .Entity<TEntity>()
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(0)::TIMESTAMP WITHOUT TIME ZONE");
    }
}
