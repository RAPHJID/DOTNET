namespace PaymentsService.Models.Dtos
{
    public class BidDto
    {
        public Guid BidId { get; set; }
        public Guid ArtId { get; set; }
        public Guid BidderId { get; set; }
        public int BidAmount { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
