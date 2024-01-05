namespace CartService.Models
{
    public class Coupon
    {
        public string CouponCode { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
