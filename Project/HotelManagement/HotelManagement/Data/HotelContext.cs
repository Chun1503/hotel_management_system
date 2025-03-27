using System;
using System.Collections.Generic;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Data;

public partial class HotelContext : DbContext
{
    public HotelContext()
    {
    }

    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Approval> Approvals { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceBooking> ServiceBookings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:HotelDbContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Approval>(entity =>
        {
            entity.HasIndex(e => e.BookingId, "IX_Approvals_BookingId");

            entity.HasIndex(e => e.ServiceBookingId, "IX_Approvals_ServiceBookingId");

            entity.HasIndex(e => e.StaffId, "IX_Approvals_StaffId");

            entity.HasOne(d => d.Booking).WithMany(p => p.Approvals).HasForeignKey(d => d.BookingId);

            entity.HasOne(d => d.ServiceBooking).WithMany(p => p.Approvals).HasForeignKey(d => d.ServiceBookingId);

            entity.HasOne(d => d.Staff).WithMany(p => p.Approvals).HasForeignKey(d => d.StaffId);
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasIndex(e => e.BookingId, "IX_Bills_BookingId")
                .IsUnique()
                .HasFilter("([BookingId] IS NOT NULL)");

            entity.HasIndex(e => e.ServiceBookingId, "IX_Bills_ServiceBookingId")
                .IsUnique()
                .HasFilter("([ServiceBookingId] IS NOT NULL)");

            entity.HasIndex(e => e.UserId, "IX_Bills_UserId");

            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Booking).WithOne(p => p.Bill).HasForeignKey<Bill>(d => d.BookingId);

            entity.HasOne(d => d.ServiceBooking).WithOne(p => p.Bill).HasForeignKey<Bill>(d => d.ServiceBookingId);

            entity.HasOne(d => d.User).WithMany(p => p.Bills).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasIndex(e => e.RoomId, "IX_Bookings_RoomId");

            entity.HasIndex(e => e.UserId, "IX_Bookings_UserId");

            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.Bookings).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasIndex(e => e.RoomId, "IX_Reviews_RoomId");

            entity.HasIndex(e => e.UserId, "IX_Reviews_UserId");

            entity.HasOne(d => d.Room).WithMany(p => p.Reviews).HasForeignKey(d => d.RoomId);

            entity.HasOne(d => d.User).WithMany(p => p.Reviews).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ServiceBooking>(entity =>
        {
            entity.HasIndex(e => e.BookingId, "IX_ServiceBookings_BookingId");

            entity.HasIndex(e => e.ServiceId, "IX_ServiceBookings_ServiceId");

            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Booking).WithMany(p => p.ServiceBookings)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceBookings)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
