using BidService.Models.DTOs;
using BidService.Services.IServices;
using Newtonsoft.Json;

namespace BidService.Services
{
    public class ArtService : IArt
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public ArtService(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }

        public async Task<ArtDto> GetArtById(Guid Id)
        {
            var client = _httpClientFactory.CreateClient("art");
            var response = await client.GetAsync(Id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDTO>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ArtDto>(responseDto.Result.ToString());
            }
            return new ArtDto();
        }

        }
    }
