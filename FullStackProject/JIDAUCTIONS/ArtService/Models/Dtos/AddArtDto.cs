namespace ArtService.Models.Dtos
{
    public class AddArtDto
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string Artist { get; set; }
        public string? Category { get; set; }
        public int? StartingPrice { get; set; }
        public int? CurrentBid { get; set; }
        public ArtStatus Status { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
    }
}
