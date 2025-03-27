using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Room
{
    public int Idroom { get; set; }

    public string Name { get; set; } = null!;

    public int IdroomType { get; set; }

    public decimal Price { get; set; }

    public string? Image { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual RoomType IdroomTypeNavigation { get; set; } = null!;
}
