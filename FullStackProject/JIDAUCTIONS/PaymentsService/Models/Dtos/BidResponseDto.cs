namespace PaymentsService.Models.Dtos
{
    public class BidResponseDto
    {
        public int BidPrice { get; set; }
        public Guid ArtId { get; set; }
        public Guid BidderId { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public ArtDto? Art { get; set; }
    }
}
