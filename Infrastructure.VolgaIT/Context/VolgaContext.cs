using Microsoft.EntityFrameworkCore;
using Domain.VolgaIT.Entities;

namespace Infrastructure.VolgaIT.Context
{
    public class VolgaContext : DbContext
    {
        public VolgaContext(DbContextOptions<VolgaContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Transport> Transports { get; set; }

        public DbSet<RentInfo> RentInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.OwnedTransport)
                .WithOne(e => e.Owner)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Transport>()
                .HasMany(e => e.RentHistory)
                .WithOne(e => e.Transport)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<RentInfo>()
                .HasOne(e => e.CurrentUser)
                .WithMany(e => e.RentHistory)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);

            modelBuilder.Entity<RentInfo>()
                .HasOne(e => e.Transport)
                .WithMany(e => e.RentHistory)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasForeignKey(e => e.TransportId)
                .IsRequired(false);
        }
    }
}
