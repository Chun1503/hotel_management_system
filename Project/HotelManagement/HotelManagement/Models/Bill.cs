using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Bill
{
    public int Idbill { get; set; }

    public string Invoice { get; set; } = null!;

    public int Idbooking { get; set; }

    public int Idaccount { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Account IdaccountNavigation { get; set; } = null!;

    public virtual Booking IdbookingNavigation { get; set; } = null!;
}
