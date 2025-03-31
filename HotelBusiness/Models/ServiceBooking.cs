using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelBusiness.Models;

[Table("ServiceBooking")]
public partial class ServiceBooking
{
    [Key]
    [Column("IDServiceBooking")]
    public int IdserviceBooking { get; set; }

    [Column("IDBooking")]
    public int Idbooking { get; set; }

    [Column("IDService")]
    public int Idservice { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("Idbooking")]
    [InverseProperty("ServiceBookings")]
    public virtual Booking IdbookingNavigation { get; set; } = null!;

    [ForeignKey("Idservice")]
    [InverseProperty("ServiceBookings")]
    public virtual Service IdserviceNavigation { get; set; } = null!;
}
