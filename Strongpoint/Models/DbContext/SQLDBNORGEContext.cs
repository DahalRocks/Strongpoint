using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Strongpoint.Models
{
    public partial class SQLDBNORGEContext : DbContext
    {
        public SQLDBNORGEContext()
        {
        }

        public SQLDBNORGEContext(DbContextOptions<SQLDBNORGEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Faktura> Faktura { get; set; }
        public virtual DbSet<Leverendør> Leverendør { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SQLDBNORGE;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faktura>(entity =>
            {
                entity.HasKey(e => e.Faktura_Nummer);

                entity.Property(e => e.Faktura_Nummer).HasColumnName("Faktura_Nummer");

                entity.Property(e => e.Datum_Intervall)
                    .HasColumnName("Datum_Intervall")
                    .HasColumnType("datetime");

                entity.Property(e => e.Leverendør_Id).HasColumnName("Leverendør_ID");
            });

            modelBuilder.Entity<Leverendør>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Attesterad_Av).HasColumnName("Attesterad_Av");

                entity.Property(e => e.Eventuella_Kommentarer).HasColumnName("Eventuella_Kommentarer");

                entity.Property(e => e.Inkjøps_Nummer)
                    .HasColumnName("Inkjøps_Nummer")
                    .HasMaxLength(50);

                entity.Property(e => e.Kunde_Nummer)
                    .HasColumnName("Kunde_Nummer")
                    .HasMaxLength(50);

                entity.Property(e => e.Løpe_Nummer)
                    .HasColumnName("Løpe_Nummer")
                    .HasMaxLength(50);
            });
        }
    }
}
