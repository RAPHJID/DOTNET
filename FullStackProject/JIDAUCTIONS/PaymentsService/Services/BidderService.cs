using Newtonsoft.Json;
using PaymentsService.Models.Dtos;
using PaymentsService.Services.IServices;

namespace PaymentsService.Services
{
    public class BidderService : IBidder
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BidderService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<BidderDto> GetUserById(string Id)
        {
            var client = _httpClientFactory.CreateClient("Bidders");
            var response = await client.GetAsync(Id);
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BidderDto>(responseDto.Result.ToString());
            }
            return new BidderDto();
        }
    }
}

