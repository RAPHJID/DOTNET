namespace CartService.Models
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public CartStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
