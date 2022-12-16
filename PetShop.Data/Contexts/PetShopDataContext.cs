using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PetShop.Data.Model;

namespace PetShop.Data.Contexts
{
    public partial class PetShopDataContext : DbContext
    {
        public PetShopDataContext()
        {
        }

        public PetShopDataContext(DbContextOptions<PetShopDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; } = null!;
        public virtual DbSet<Categry> Categries { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-6S42NQ6; Initial Catalog=PetShop; Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Animal__Category__267ABA7A");
            });

            modelBuilder.Entity<Categry>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__Categrie__19093A0B6336B3FA");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.Animel)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AnimelId)
                    .HasConstraintName("FK__Comment__AnimelI__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
