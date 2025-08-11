using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Test.MyApp.Domain.Common;

namespace Test.MyApp.Domain.EntityModels;

public partial class MyAppDbContext : DbContext
{
    public MyAppDbContext()
    {
    }

    public MyAppDbContext(DbContextOptions<MyAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<SeatColor> SeatColors { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(AppConfiguration.ConnectionString.DefaultConnection);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seat__3214EC07A4FB88C6");

            entity.HasOne(d => d.SeatColor).WithMany(p => p.Seats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Seat__SeatColorI__398D8EEE");
        });

        modelBuilder.Entity<SeatColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeatColo__3214EC074894CD25");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07F4DA2E95");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
