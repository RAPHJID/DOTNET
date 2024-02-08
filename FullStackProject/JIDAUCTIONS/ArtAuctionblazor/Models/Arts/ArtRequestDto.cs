using System.ComponentModel.DataAnnotations;

namespace ArtAuctionblazor.Models.Arts
{
    public class ArtRequestDto
    {
        public Guid ArtId { get; set; }
      
        public string Name { get; set; }
        
        public string Description { get; set; }
      
        public int Price { get; set; }
       
        public string Category { get; set; }
        
        public string Artist { get; set; }
        
        public string Status { get; set; }
       
        public int CurrentBid { get; set; }
       
        public string ImageUrl { get; set; }
      
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);
    }
}
