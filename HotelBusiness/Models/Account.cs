using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelBusiness.Models;

[Table("Account")]
[Index("Idemail", Name = "UQ__Account__AACC2A9296C4F83D", IsUnique = true)]
[Index("UserName", Name = "UQ__Account__C9F28456BA935FA0", IsUnique = true)]
[Index("Idfacebook", Name = "UQ__Account__D5B5CF457A77E277", IsUnique = true)]
public partial class Account
{
    [Key]
    [Column("IDAccount")]
    public int Idaccount { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string UserName { get; set; } = null!;

    [StringLength(255)]
    public string PassWord { get; set; } = null!;

    [StringLength(10)]
    public string Gender { get; set; } = null!;

    [StringLength(15)]
    public string PhoneNumber { get; set; } = null!;

    [Column("IDEmail")]
    [StringLength(255)]
    public string Idemail { get; set; } = null!;

    [Column("IDFacebook")]
    [StringLength(255)]
    public string? Idfacebook { get; set; }

    [StringLength(255)]
    public string? Bank { get; set; }

    [StringLength(50)]
    public string? BankNumber { get; set; }

    [StringLength(50)]
    public string Role { get; set; } = null!;

    [StringLength(50)]
    public string Status { get; set; } = null!;

    [InverseProperty("IdaccountNavigation")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    [InverseProperty("IdaccountNavigation")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
