using ArtAuctionblazor.Models;
using ArtAuctionblazor.Models.Auth;
using Newtonsoft.Json;
using System.Text;

namespace ArtAuctionblazor.Services.Auth
{
    public class AuthService : IAuth
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "https://localhost:7003";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var request = JsonConvert.SerializeObject(loginRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/User/Login", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                return JsonConvert.DeserializeObject<LoginResponseDto>(results.Result.ToString());

            }
            return new LoginResponseDto();
        }

        public async Task<ResponseDto> Register(RegisterRequestDto registerRequestDto)
        {
            var request = JsonConvert.SerializeObject(registerRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/User/Register", bodyContent);
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
