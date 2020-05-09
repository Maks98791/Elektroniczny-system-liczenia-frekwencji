using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CounterWebApp.Models
{
    public partial class GuestCounterContext : DbContext
    {
        public GuestCounterContext()
        {
        }

        public GuestCounterContext(DbContextOptions<GuestCounterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cameras> Cameras { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Visitors> Visitors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=GuestCounter;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cameras>(entity =>
            {
                entity.HasKey(e => e.CameraId)
                    .HasName("PK__Cameras__29277C9CB7941ED5");

                entity.Property(e => e.CameraId).HasColumnName("camera_id");

                entity.Property(e => e.CameraLocation)
                    .IsRequired()
                    .HasColumnName("camera_location")
                    .HasMaxLength(63);
            });

            modelBuilder.Entity<Photos>(entity =>
            {
                entity.HasKey(e => e.PhotoId)
                    .HasName("PK__Photos__CB48C83DFD681095");

                entity.Property(e => e.PhotoId).HasColumnName("photo_id");

                entity.Property(e => e.CameraId).HasColumnName("camera_id");

                entity.Property(e => e.PhotoPath)
                    .IsRequired()
                    .HasColumnName("photo_path")
                    .HasMaxLength(255);

                entity.Property(e => e.RaportDate)
                    .HasColumnName("raport_date")
                    .HasColumnType("smalldatetime");

                entity.HasOne(d => d.Camera)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.CameraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Photos_ID_FK");
            });

            modelBuilder.Entity<Visitors>(entity =>
            {
                entity.HasKey(e => e.RaportId)
                    .HasName("PK__Visitors__008104D4754AE775");

                entity.Property(e => e.RaportId).HasColumnName("raport_id");

                entity.Property(e => e.CameraId).HasColumnName("camera_id");

                entity.Property(e => e.GuestsIn).HasColumnName("guests_in");

                entity.Property(e => e.GuestsOut).HasColumnName("guests_out");

                entity.Property(e => e.RaportDate)
                    .HasColumnName("raport_date")
                    .HasColumnType("smalldatetime");

                entity.HasOne(d => d.Camera)
                    .WithMany(p => p.Visitors)
                    .HasForeignKey(d => d.CameraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Visitors_ID_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
