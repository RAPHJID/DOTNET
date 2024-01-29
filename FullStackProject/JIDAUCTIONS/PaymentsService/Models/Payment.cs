namespace PaymentsService.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid BidId { get; set; }
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid BidderId { get; set; }
    }
}
