using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Approval
{
    public int ApprovalId { get; set; }

    public int? StaffId { get; set; }

    public int? BookingId { get; set; }

    public int? ServiceBookingId { get; set; }

    public string ApprovalStatus { get; set; } = null!;

    public DateTime ApprovedAt { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual ServiceBooking? ServiceBooking { get; set; }

    public virtual User? Staff { get; set; }
}
