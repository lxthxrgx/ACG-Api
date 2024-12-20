using System;
using Microsoft.EntityFrameworkCore;
using ACG_Class.Model.Class;
using ACG_Class.Model.ModelMemory.Class;

namespace ACG_Class.Database
{
    public class MemoryDb : DbContext
    {
        public DbSet<_1D_Memory> D1_Memory { get; set; }
        public DbSet<_2D_Memory> D2_Memory { get; set; }
        public DbSet<_4D_Memory> D4_Memory { get; set; }
        public DbSet<_5D_Memory> D5_Memory { get; set; }

        public MemoryDb(DbContextOptions<MemoryDb> options) : base(options) { }
        public MemoryDb() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<_2D_Memory>()
                .HasOne(d => d.SubleaseDop)
                .WithOne(sd => sd._2D)
                .HasForeignKey<_2D_Memory>(d => d.SubleaseDopId);

            modelBuilder.Entity<_4D_Memory>()
               .HasMany(e => e.PathToPdfFiles_Sublease_Memory)
               .WithOne(e => e._4D)
               .HasForeignKey(e => e._4DId)
               .IsRequired();

            modelBuilder.Entity<_5D_Memory>()
                .HasMany(e => e.PathToFilesGuard)
                .WithOne(e => e.D5class)
                .HasForeignKey(e => e._5dId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
