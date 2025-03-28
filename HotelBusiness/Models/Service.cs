using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelBusiness.Models;

[Table("Service")]
public partial class Service
{
    [Key]
    [Column("IDService")]
    public int Idservice { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = null!;

    [InverseProperty("IdserviceNavigation")]
    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();
}
