using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelBusiness.Models;

[Table("RoomType")]
public partial class RoomType
{
    [Key]
    [Column("IDRoomType")]
    public int IdroomType { get; set; }

    [StringLength(100)]
    public string TypeName { get; set; } = null!;

    [InverseProperty("IdroomTypeNavigation")]
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
