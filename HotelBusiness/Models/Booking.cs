using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelBusiness.Models;

[Table("Booking")]
public partial class Booking
{
    [Key]
    [Column("IDBooking")]
    public int Idbooking { get; set; }

    [Column("IDAccount")]
    public int Idaccount { get; set; }

    [Column("IDRoom")]
    public int Idroom { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Deposit { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = null!;

    [StringLength(500)]
    public string? Note { get; set; }

    [InverseProperty("IdbookingNavigation")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    [ForeignKey("Idaccount")]
    [InverseProperty("Bookings")]
    public virtual Account IdaccountNavigation { get; set; } = null!;

    [ForeignKey("Idroom")]
    [InverseProperty("Bookings")]
    public virtual Room IdroomNavigation { get; set; } = null!;

    [InverseProperty("IdbookingNavigation")]
    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();
}
