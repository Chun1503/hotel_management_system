using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class ServiceBooking
{
    public int IdserviceBooking { get; set; }

    public int Idbooking { get; set; }

    public int Idservice { get; set; }

    public int Quantity { get; set; }

    public virtual Booking IdbookingNavigation { get; set; } = null!;

    public virtual Service IdserviceNavigation { get; set; } = null!;
}
