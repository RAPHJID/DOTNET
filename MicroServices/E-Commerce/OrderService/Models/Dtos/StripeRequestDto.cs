namespace OrderService.Models.Dtos
{
    public class StripeRequestDto
    {
        public string StripeSessionUrl { get; set; } = String.Empty;
        public string StripeSessionId { get; set; } = String.Empty;
        public string ApprovedUrl { get; set; } = String.Empty;
        public string CancelUrl { get; set; } = String.Empty;
        public Guid OrderId { get; set; }
    }
}
