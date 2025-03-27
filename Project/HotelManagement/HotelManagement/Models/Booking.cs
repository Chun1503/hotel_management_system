using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Booking
{
    public int Idbooking { get; set; }

    public int Idaccount { get; set; }

    public int Idroom { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal Deposit { get; set; }

    public string Status { get; set; } = null!;

    public string? Note { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual Account IdaccountNavigation { get; set; } = null!;

    public virtual Room IdroomNavigation { get; set; } = null!;

    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();
}
