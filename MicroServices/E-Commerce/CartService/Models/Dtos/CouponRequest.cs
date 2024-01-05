namespace CartService.Models.Dtos
{
    public class CouponRequest
    {
        public Guid CartId { get; set; } 
        public string CouponCode { get; set; } = string.Empty;
    }
}
