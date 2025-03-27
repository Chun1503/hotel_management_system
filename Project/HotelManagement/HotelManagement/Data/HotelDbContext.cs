using System;
using System.Collections.Generic;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Data;

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
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:HotelDbContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Idaccount).HasName("PK__Account__1D323F90B2A92290");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Idemail, "UQ__Account__AACC2A92C02BB583").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Account__C9F284564FDCB7E0").IsUnique();

            entity.HasIndex(e => e.Idfacebook, "UQ__Account__D5B5CF452AB46A26").IsUnique();

            entity.Property(e => e.Idaccount).HasColumnName("IDAccount");
            entity.Property(e => e.Bank).HasMaxLength(255);
            entity.Property(e => e.BankNumber).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Idemail)
                .HasMaxLength(255)
                .HasColumnName("IDEmail");
            entity.Property(e => e.Idfacebook)
                .HasMaxLength(255)
                .HasColumnName("IDFacebook");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PassWord).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Idbill).HasName("PK__Bill__23BDC1E6D0C0AC73");

            entity.ToTable("Bill");

            entity.Property(e => e.Idbill).HasColumnName("IDBill");
            entity.Property(e => e.Idaccount).HasColumnName("IDAccount");
            entity.Property(e => e.Idbooking).HasColumnName("IDBooking");
            entity.Property(e => e.Invoice).HasMaxLength(100);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdaccountNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Idaccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__IDAccount__46E78A0C");

            entity.HasOne(d => d.IdbookingNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Idbooking)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__IDBooking__47DBAE45");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Idbooking).HasName("PK__Booking__B20896CFE8D62DCE");

            entity.ToTable("Booking");

            entity.Property(e => e.Idbooking).HasColumnName("IDBooking");
            entity.Property(e => e.Deposit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Idaccount).HasColumnName("IDAccount");
            entity.Property(e => e.Idroom).HasColumnName("IDRoom");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.IdaccountNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Idaccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__IDAccou__48CFD27E");

            entity.HasOne(d => d.IdroomNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Idroom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__IDRoom__49C3F6B7");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Idroom).HasName("PK__Room__A1BA0EAFC599A38B");

            entity.ToTable("Room");

            entity.Property(e => e.Idroom).HasColumnName("IDRoom");
            entity.Property(e => e.IdroomType).HasColumnName("IDRoomType");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.IdroomTypeNavigation).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.IdroomType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room__IDRoomType__4AB81AF0");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.IdroomType).HasName("PK__RoomType__FCDD8EA68DCE4E68");

            entity.ToTable("RoomType");

            entity.Property(e => e.IdroomType).HasColumnName("IDRoomType");
            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Idservice).HasName("PK__Service__5049E73AB49A9205");

            entity.ToTable("Service");

            entity.Property(e => e.Idservice).HasColumnName("IDService");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<ServiceBooking>(entity =>
        {
            entity.HasKey(e => e.IdserviceBooking).HasName("PK__ServiceB__C805DA7D1161D547");

            entity.ToTable("ServiceBooking");

            entity.Property(e => e.IdserviceBooking).HasColumnName("IDServiceBooking");
            entity.Property(e => e.Idbooking).HasColumnName("IDBooking");
            entity.Property(e => e.Idservice).HasColumnName("IDService");
            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.IdbookingNavigation).WithMany(p => p.ServiceBookings)
                .HasForeignKey(d => d.Idbooking)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceBo__IDBoo__4BAC3F29");

            entity.HasOne(d => d.IdserviceNavigation).WithMany(p => p.ServiceBookings)
                .HasForeignKey(d => d.Idservice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceBo__IDSer__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
