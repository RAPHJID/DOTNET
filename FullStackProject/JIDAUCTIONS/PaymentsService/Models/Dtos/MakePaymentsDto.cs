namespace PaymentsService.Models.Dtos
{
    public class MakePaymentsDto
    {
        public Guid ArtId { get; set; }
        public Guid BidId { get; set; }
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid BidderId { get; set; }
    }
}
