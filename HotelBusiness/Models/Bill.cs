using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelBusiness.Models;

[Table("Bill")]
public partial class Bill
{
    [Key]
    [Column("IDBill")]
    public int Idbill { get; set; }

    [StringLength(100)]
    public string Invoice { get; set; } = null!;

    [Column("IDBooking")]
    public int Idbooking { get; set; }

    [Column("IDAccount")]
    public int Idaccount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PaymentDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalPrice { get; set; }

    [ForeignKey("Idaccount")]
    [InverseProperty("Bills")]
    public virtual Account IdaccountNavigation { get; set; } = null!;

    [ForeignKey("Idbooking")]
    [InverseProperty("Bills")]
    public virtual Booking IdbookingNavigation { get; set; } = null!;
}
