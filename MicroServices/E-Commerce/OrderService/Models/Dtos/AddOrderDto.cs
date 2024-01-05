namespace OrderService.Models.Dtos
{
    public class AddOrderDto
    {
        public Guid UserId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public double TotalAmount { get; set; }
        public Guid ProductId { get; set; }
    }
}
