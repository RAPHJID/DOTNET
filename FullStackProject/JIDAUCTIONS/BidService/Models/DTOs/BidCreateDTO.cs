using System.ComponentModel.DataAnnotations;

namespace BidService.Models.DTOs
{
    public class BidCreateDTO
    {
        [Required] 
        public Guid ArtId { get; set; }
        [Required]
        public Guid BidderId { get; set; }
        [Required]
        public int BidAmount { get; set; }
    }
}
