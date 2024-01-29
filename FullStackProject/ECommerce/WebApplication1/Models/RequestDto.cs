

using static WebApplication1.Utility.StaticDetails;

namespace WebApplication1.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url {  get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
