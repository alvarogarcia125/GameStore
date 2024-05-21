using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Game> Game { get; set; }

        public DbSet<GameGenre> GameGenre { get; set; }

        public DbSet<GamePlatform> GamePlatform { get; set; }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<Platform> Platform { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasKey(g => g.Id);
            modelBuilder.Entity<Game>().Property(g => g.Name).IsRequired();
            modelBuilder.Entity<Game>().Property(g => g.Key).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Game>().HasIndex(g => g.Key).IsUnique();
            modelBuilder.Entity<Game>().Property(g => g.Description).IsRequired(false);

            modelBuilder.Entity<Genre>().HasKey(g => g.Id);
            modelBuilder.Entity<Genre>().Property(g => g.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Genre>().HasIndex(g => g.Name).IsUnique();
            modelBuilder.Entity<Genre>().Property(g => g.ParentGenreId).IsRequired(false);

            modelBuilder.Entity<Platform>().HasKey(p => p.Id);
            modelBuilder.Entity<Platform>().Property(p => p.Type).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Platform>().HasIndex(p => p.Type).IsUnique();

            modelBuilder.Entity<GameGenre>().HasKey(gp => gp.Id);
            modelBuilder.Entity<GameGenre>().Property(gg => gg.GameId).IsRequired();
            modelBuilder.Entity<GameGenre>().Property(gg => gg.GenreId).IsRequired();
            modelBuilder.Entity<GameGenre>().HasOne(o => o.Game).WithMany(c => c.Genres).HasForeignKey(o => o.GameId);
            modelBuilder.Entity<GameGenre>().HasIndex(gp => new { gp.GameId, gp.GenreId }).IsUnique();

            modelBuilder.Entity<GamePlatform>().HasKey(gp => gp.Id);
            modelBuilder.Entity<GamePlatform>().Property(gg => gg.GameId).IsRequired();
            modelBuilder.Entity<GamePlatform>().Property(gg => gg.PlatformId).IsRequired();
            modelBuilder.Entity<GamePlatform>().HasOne(o => o.Game).WithMany(c => c.Platforms).HasForeignKey(o => o.GameId);
            modelBuilder.Entity<GamePlatform>().HasIndex(gp => new { gp.GameId, gp.PlatformId }).IsUnique();

            Guid StrategyGuid = Guid.NewGuid();
            Guid RacesGuid = Guid.NewGuid();
            Guid ActionGuid = Guid.NewGuid();

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = StrategyGuid, Name = "Strategy", ParentGenreId = null },
                new Genre { Id = Guid.NewGuid(), Name = "RTS", ParentGenreId = StrategyGuid },
                new Genre { Id = Guid.NewGuid(), Name = "TBS", ParentGenreId = StrategyGuid },
                new Genre { Id = Guid.NewGuid(), Name = "RPG", ParentGenreId = StrategyGuid },
                new Genre { Id = Guid.NewGuid(), Name = "Sports", ParentGenreId = null },
                new Genre { Id = RacesGuid, Name = "Races", ParentGenreId = null },
                new Genre { Id = Guid.NewGuid(), Name = "Rally", ParentGenreId = RacesGuid },
                new Genre { Id = Guid.NewGuid(), Name = "Arcade", ParentGenreId = RacesGuid },
                new Genre { Id = Guid.NewGuid(), Name = "Formula", ParentGenreId = RacesGuid },
                new Genre { Id = Guid.NewGuid(), Name = "Off-road", ParentGenreId = RacesGuid },
                new Genre { Id = ActionGuid, Name = "Action", ParentGenreId = null },
                new Genre { Id = Guid.NewGuid(), Name = "FPS", ParentGenreId = ActionGuid },
                new Genre { Id = Guid.NewGuid(), Name = "TPS", ParentGenreId = ActionGuid },
                new Genre { Id = Guid.NewGuid(), Name = "Adventure", ParentGenreId = null },
                new Genre { Id = Guid.NewGuid(), Name = "Puzzle & Skill", ParentGenreId = null }
            );

            modelBuilder.Entity<Platform>().HasData(
                new Platform { Id = Guid.NewGuid(), Type = "Mobile" },
                new Platform { Id = Guid.NewGuid(), Type = "Browser" },
                new Platform { Id = Guid.NewGuid(), Type = "Desktop" },
                new Platform { Id = Guid.NewGuid(), Type = "Console" }
            );

        }


    }
}
