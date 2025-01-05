using Microsoft.EntityFrameworkCore;
using BibliotecaUtad.Models;

namespace BibliotecaUtad.Data
{
    public class BibliotecaUtadContext : DbContext
    {
        public BibliotecaUtadContext(DbContextOptions<BibliotecaUtadContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Gender> Gender { get; set; } = default!;
        public DbSet<Subgender> Subgender { get; set; } = default!;
        public DbSet<Librabry> Library { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Gender)
                .WithMany()
                .HasForeignKey(b => b.GenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Subgender)
                .WithMany()
                .HasForeignKey(b => b.SubGenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<BibliotecaUtad.Models.Librabry> Librabry { get; set; } = default!;
    }
}