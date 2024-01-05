using Newtonsoft.Json;
using OrderService.Models.Dtos;
using OrderService.Services.IServices;

namespace OrderService.Services
{
    public class ProductService : IProduct
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ProductDto> GetById(Guid id)
        {
            var client = _httpClientFactory.CreateClient("Products");
            var response = await client.GetAsync(id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductDto>(responseDto.Result.ToString());
            }
            return new ProductDto();
        }
    }
}
