namespace BLAZORECOMMERCE.Components.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public int CouponAmount { get; set; } 
        public int CouponMinAmount { get; set; }
    }
}
