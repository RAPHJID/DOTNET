namespace OrderService.Models.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription {get; set;}
        public DateTime PurchasedTime { get; set; }
        public int Price { get; set; }
    }
}
