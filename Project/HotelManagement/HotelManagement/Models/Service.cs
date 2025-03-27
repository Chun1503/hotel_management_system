using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Service
{
    public int Idservice { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();
}
