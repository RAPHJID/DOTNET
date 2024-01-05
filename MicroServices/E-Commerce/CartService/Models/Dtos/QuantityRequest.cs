namespace CartService.Models.Dtos
{
    public class QuantityRequest
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
