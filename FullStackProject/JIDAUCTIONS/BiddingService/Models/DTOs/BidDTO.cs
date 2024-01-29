namespace BiddingService.Models.DTOs
{
    public class BidDTO
    {
        public Guid BidId { get; set; }
        public Guid ArtId { get; set; }
        public Guid BidderId { get; set; }
        public Guid SellerId { get; set; }
        public int BidAmount { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
