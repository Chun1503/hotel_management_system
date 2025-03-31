using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelBusiness.Models;

[Table("Room")]
public partial class Room
{
    [Key]
    [Column("IDRoom")]
    public int Idroom { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("IDRoomType")]
    public int IdroomType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [StringLength(255)]
    public string? Image { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = null!;

    [InverseProperty("IdroomNavigation")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [ForeignKey("IdroomType")]
    [InverseProperty("Rooms")]
    public virtual RoomType IdroomTypeNavigation { get; set; } = null!;
}
