using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Account
{
    public int Idaccount { get; set; }

    public string Name { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Idemail { get; set; } = null!;

    public string? Idfacebook { get; set; }

    public string? Bank { get; set; }

    public string? BankNumber { get; set; }

    public string Role { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
