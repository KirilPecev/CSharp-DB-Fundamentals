namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {

        }

        public FootballBettingContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasOne(e => e.PrimaryKitColor)
                    .WithMany(e => e.PrimaryKitTeams)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SecondaryKitColor)
                   .WithMany(e => e.SecondaryKitTeams)
                   .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Town)
                      .WithMany(e => e.Teams);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasOne(e => e.HomeTeam)
                    .WithMany(e => e.HomeGames)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.AwayTeam)
                    .WithMany(e => e.AwayGames)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasOne(e => e.Country)
                    .WithMany(e => e.Towns);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasOne(e => e.Team)
                    .WithMany(e => e.Players);

                entity.HasOne(e => e.Position)
                    .WithMany(e => e.Players);
            });

            modelBuilder.Entity<PlayerStatistic>(entity =>
            {
                entity.HasKey(k => new { k.PlayerId, k.GameId });

                entity.HasOne(e => e.Player)
                    .WithMany(e => e.PlayerStatistics);

                entity.HasOne(e => e.Game)
                    .WithMany(e => e.PlayerStatistics);
            });

            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasOne(e => e.Game)
                    .WithMany(e => e.Bets);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Bets);
            });
        }
    }
}
