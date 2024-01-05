namespace OrderService.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public double TotalAmount { get; set; }
        public Guid ProductId { get; set; }
        public string StripeSessionId { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public string PaymentIntent { get; set; } = string.Empty;


    }
}
