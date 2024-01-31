/*using Newtonsoft.Json;
using PaymentsService.Models.Dtos;
using PaymentsService.Services.IServices;

namespace PaymentsService.Services
{
    public class BidService : IBid
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BidService(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }
        public async Task<BidResponseDto> GetBidById(Guid Id)
        {
            var client = _httpClientFactory.CreateClient("Bid");
            var response = await client.GetAsync($"{Id}");

            var context = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<BidResponseDto>(context);
            if (response.IsSuccessStatusCode)
            {
                return responseDto;
            }
            return new BidResponseDto();
        }
    }
}*/
using Newtonsoft.Json;
using PaymentsService.Models.Dtos;
using PaymentsService.Services.IServices;
using System;

namespace PaymentsService.Services
{
    public class BidService : IBid
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BidService(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }

        public async Task<BidResponseDto> GetBidById(Guid Id)
        {
            var client = _httpClientFactory.CreateClient("Bid");

            // Ensure the URI is constructed properly, either absolute or relative to the BaseAddress
            var requestUri = $"/api/Bid/{Id}";

            var response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var context = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<BidResponseDto>(context);
                return responseDto;
            }

            return new BidResponseDto();
        }
    }
}

