namespace TicketMS.Models.Dto
{
    public class OrderDto
    {
     //   public Guid Id { get; set; }
        public int OrderId { get; set; }

        public int? NumberOfTickets { get; set; } 

        public DateTime? OrderedAt { get; set; }

        public int? TotalPrice { get; set; }

        public string Customer { get; set; }
        public  string TicketCategory { get; set; }   


    }
}
