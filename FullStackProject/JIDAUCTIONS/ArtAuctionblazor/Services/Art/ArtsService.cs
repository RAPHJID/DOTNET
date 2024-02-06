using ArtAuctionblazor.Models;
using ArtAuctionblazor.Models.Arts;
using Newtonsoft.Json;
using System.Text;

namespace ArtAuctionblazor.Services.Art
{
    public class ArtsService : IArt
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "https://localhost:7125";
        public ArtsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseDto> AddArt(ArtRequestDto art)
        {
            var request = JsonConvert.SerializeObject(art);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BASEURL}/api/Art", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                
                return results;

            }

            return new ResponseDto();
        }

        public async  Task<ResponseDto> deleteArt(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{BASEURL}/api/Art?id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                
                return results;

            }

            return new ResponseDto();
        }

        public async  Task<ArtDto> GetArtByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Art/GetById/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<ArtDto>(results.Result.ToString());

            }
            return new ArtDto();
        }

        public async Task<List<ArtDto>> GetArtsAsync()
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Art");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
              
                return JsonConvert.DeserializeObject<List<ArtDto>>(results.Result.ToString());

            }
            return new List<ArtDto>();
        }

       

        public async Task<ResponseDto> updateArt(Guid id, ArtRequestDto artRequestDto)
        {
            var request = JsonConvert.SerializeObject(artRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BASEURL}/api/Art?id={id}", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
               
                return results;

            }

            return new ResponseDto();
        }
    }
}
