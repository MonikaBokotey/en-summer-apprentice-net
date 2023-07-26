using System;
using System.Collections.Generic;

namespace TicketMS.Models;

public partial class TicketCategory
{
    public int TicketCategoryId { get; set; }

    public string? Description { get; set; }

    public int Price { get; set; }

    public int? EventId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
