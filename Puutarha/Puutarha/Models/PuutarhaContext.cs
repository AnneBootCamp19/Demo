using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Puutarha.Models
{
    public partial class PuutarhaContext : DbContext
    {
        public PuutarhaContext()
        {
        }

        public PuutarhaContext(DbContextOptions<PuutarhaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Istutukset> Istutukset { get; set; }
        public virtual DbSet<Kasvit> Kasvit { get; set; }
        public virtual DbSet<Satotiedot> Satotiedot { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Puutarha;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Istutukset>(entity =>
            {
                entity.HasKey(e => e.IstutusId);

                entity.Property(e => e.IstutusId)
                    .HasColumnName("IstutusID")
                    .ValueGeneratedOnAdd(); 

                  //  .ValueGeneratedNever();

                entity.Property(e => e.IstutusPvm)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Istutuspaikka).HasMaxLength(10);

                entity.Property(e => e.KasviId).HasColumnName("KasviID");

                entity.Property(e => e.Lisätieto).HasMaxLength(300);

                entity.Property(e => e.Lämpötila).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Määrä).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Poistopvm).HasColumnType("date");

                entity.Property(e => e.Yksikkö).HasMaxLength(10);

                entity.HasOne(d => d.Kasvi)
                    .WithMany(p => p.Istutukset)
                    .HasForeignKey(d => d.KasviId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Istutukset_Kasvit");
            });

            modelBuilder.Entity<Kasvit>(entity =>
            {
                entity.HasKey(e => e.KasviId);

                entity.Property(e => e.KasviId)
                    .HasColumnName("KasviID")
                    .ValueGeneratedOnAdd();
                   // .ValueGeneratedNever();

                entity.Property(e => e.Lajike).HasMaxLength(50);

                entity.Property(e => e.Nimi)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.TieteellinenNimi).HasMaxLength(50);
            });

            modelBuilder.Entity<Satotiedot>(entity =>
            {
                entity.HasKey(e => e.SatoId);

                entity.Property(e => e.SatoId)
                    .HasColumnName("SatoID")
                    .ValueGeneratedOnAdd();
                   // .ValueGeneratedNever();

                entity.Property(e => e.KasviId).HasColumnName("KasviID");

                entity.Property(e => e.Lisätieto).HasMaxLength(300);

                entity.Property(e => e.Lämpötila).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Määrä).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.SatoPvm).HasColumnType("date");

                entity.Property(e => e.Yksikkö)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Kasvi)
                    .WithMany(p => p.Satotiedot)
                    .HasForeignKey(d => d.KasviId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Satotiedot_Kasvit");
            });
        }
    }
}
