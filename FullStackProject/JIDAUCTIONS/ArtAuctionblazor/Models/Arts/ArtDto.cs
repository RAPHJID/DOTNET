using System.ComponentModel.DataAnnotations;

namespace ArtAuctionblazor.Models.Arts
{
    public class ArtDto
    {
        public Guid ArtId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int CurrentBid { get; set; }
        [Required]
        public int BidAmount { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);

    }
}
