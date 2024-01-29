namespace BidService.Models
{
    public class Bid
    {
        public Guid BidId { get; set; }
        public Guid ArtId { get; set; }
        public Guid BidderId { get; set; }
        public int BidAmount { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid UserId { get; internal set; }
    }
}
