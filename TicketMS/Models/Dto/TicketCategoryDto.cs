namespace TicketMS.Models.Dto
{
    public class TicketCategoryDto
    {
        public int TicketCategoryId { get; set; }

        public string? Description { get; set; }

        public int Price { get; set; }

        public int? EventId { get; set; }

        public virtual Event? Event { get; set; }
    }
}
