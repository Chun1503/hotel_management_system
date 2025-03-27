using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class ServiceBooking
{
    public int ServiceBookingId { get; set; }

    public int BookingId { get; set; }

    public int ServiceId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Approval> Approvals { get; set; } = new List<Approval>();

    public virtual Bill? Bill { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
