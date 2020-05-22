using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TP2_ASP.NETCORE.Models
{
    public partial class dotnetCoreTpContext : DbContext
    {
        public dotnetCoreTpContext()
        {
        }

        public dotnetCoreTpContext(DbContextOptions<dotnetCoreTpContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(50);

                entity.Property(e => e.Prenom)
                    .HasColumnName("prenom")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
