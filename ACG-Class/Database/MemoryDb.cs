using System;
using Microsoft.EntityFrameworkCore;
using ACG_Class.Model.Class;

namespace ACG_Class.Database
{
    public class MemoryDb : DbContext
    {
        public DbSet<_1D> D1_Memory { get; set; }
        public DbSet<_2D> D2_Memory { get; set; }
        public DbSet<_4D> D4_Memory { get; set; }
        public DbSet<_5D> D5_Memory { get; set; }

        public MemoryDb(DbContextOptions<MemoryDb> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<_2D>()
                .HasOne(d => d.SubleaseDop)
                .WithOne(sd => sd._2D)
                .HasForeignKey<_2D>(d => d.SubleaseDopId);

            modelBuilder.Entity<_4D>()
               .HasMany(e => e.PathToPdfFiles_Sublease)
               .WithOne(e => e._4D)
               .HasForeignKey(e => e._4DId)
               .IsRequired();

            modelBuilder.Entity<_5D>()
                .HasMany(e => e.PathToFilesGuard)
                .WithOne(e => e.D5class)
                .HasForeignKey(e => e._5dId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
