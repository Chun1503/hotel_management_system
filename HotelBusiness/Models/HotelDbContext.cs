using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelBusiness.Models;

public partial class HotelDbContext : DbContext
{
    public HotelDbContext()
    {
    }

    public HotelDbContext(DbContextOptions<HotelDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceBooking> ServiceBookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DB"));
        }
    }
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Idaccount).HasName("PK__Account__1D323F902B128F83");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Idbill).HasName("PK__Bill__23BDC1E6452D26CA");

            entity.HasOne(d => d.IdaccountNavigation).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__IDAccount__4CA06362");

            entity.HasOne(d => d.IdbookingNavigation).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__IDBooking__4BAC3F29");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Idbooking).HasName("PK__Booking__B20896CF78F1EF59");

            entity.HasOne(d => d.IdaccountNavigation).WithMany(p => p.Bookings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__IDAccou__4316F928");

            entity.HasOne(d => d.IdroomNavigation).WithMany(p => p.Bookings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__IDRoom__440B1D61");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Idroom).HasName("PK__Room__A1BA0EAFB84BE41B");

            entity.HasOne(d => d.IdroomTypeNavigation).WithMany(p => p.Rooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room__IDRoomType__3E52440B");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.IdroomType).HasName("PK__RoomType__FCDD8EA6963BA290");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Idservice).HasName("PK__Service__5049E73A8C83660D");
        });

        modelBuilder.Entity<ServiceBooking>(entity =>
        {
            entity.HasKey(e => e.IdserviceBooking).HasName("PK__ServiceB__C805DA7D7012D3EC");

            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.IdbookingNavigation).WithMany(p => p.ServiceBookings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceBo__IDBoo__47DBAE45");

            entity.HasOne(d => d.IdserviceNavigation).WithMany(p => p.ServiceBookings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceBo__IDSer__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
