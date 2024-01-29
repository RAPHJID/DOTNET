namespace ArtAuctionblazor.Models
{
    public class Art
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Artist { get; set; }
        public string? Category { get; set; }
        public int StartingPrice { get; set; }
        public int CurrentBid {  get; set; }
        public string Status { get; set; }
        public int BidAmount { get; set; }
        public string ImageUrl { get; set; }
        public DateTime EndDate { get; set; }
    }
}
