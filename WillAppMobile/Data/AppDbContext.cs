using Microsoft.EntityFrameworkCore;
using WillAppMobile.Models; // Modellerinizin bulunduğu namespace'i doğru şekilde ayarlayın

namespace WillAppMobile.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Will> Wills { get; set; }
        public DbSet<Tombstone> Tombstones { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Executor> Executors { get; set; }  // Executor modelinizi ekledim

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite veritabanına bağlanacak şekilde yapılandırma
            optionsBuilder.UseSqlite("Filename=WillAppMobile.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Wills)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<Will>()
                .HasOne(w => w.Executor)
                .WithMany()
                .HasForeignKey(w => w.ExecutorId);

            modelBuilder.Entity<Tombstone>()
                .HasMany(t => t.Posts)
                .WithOne(p => p.Tombstone)
                .HasForeignKey(p => p.TombstoneId);

            // Diğer yapılandırmaları burada ekleyebilirsiniz
        }
    }
}
