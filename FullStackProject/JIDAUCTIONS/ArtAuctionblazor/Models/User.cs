namespace ArtAuctionblazor.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsBidder { get; set; }
        public bool IsSeller { get; set; }
        public int NoOfBids { get; set; }
    }
}
