using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public int UserId { get; set; }

    public int? BookingId { get; set; }

    public int? ServiceBookingId { get; set; }

    public decimal TotalAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual ServiceBooking? ServiceBooking { get; set; }

    public virtual User User { get; set; } = null!;
}
