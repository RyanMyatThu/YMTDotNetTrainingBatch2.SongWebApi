using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace YMTDotNetTrainingBatch2.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblSong> TblSongs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=SongsData;User ID=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblSong>(entity =>
        {
            entity.ToTable("Tbl_Songs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Artist)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Genres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReleasedDate).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
