﻿namespace PaymentsService.Models.Dtos
{
    public class StripeRequestDto
    {
        public string? StripeSessionUrl { get; set; }
        public string? StripeSessionId { get; set; }
        public string? ApprovedUrl { get; set; }
        public string? CancelUrl { get; set; }
        public Guid PaymentId { get; set; }
    }
}
