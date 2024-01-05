namespace ProductService.Models.Dtos
{
    public class AddProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; }= string.Empty;
        public DateTime PurchasedTime { get; set; }
        public int Price { get; set; }
    }
}
