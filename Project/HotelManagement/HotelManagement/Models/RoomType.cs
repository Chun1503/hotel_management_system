using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class RoomType
{
    public int IdroomType { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
