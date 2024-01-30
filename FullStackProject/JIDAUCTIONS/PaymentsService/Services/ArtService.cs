using Newtonsoft.Json;
using PaymentsService.Models.Dtos;
using PaymentsService.Services.IServices;

namespace PaymentsService.Services
{
    public class ArtService : IArt
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ArtService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ArtDto> GetById(Guid Id)
        {
            var client = _httpClientFactory.CreateClient("Arts");
            var response = await client.GetAsync(Id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ArtDto>(responseDto.Result.ToString());
            }
            return new ArtDto();
        }
    }
}
