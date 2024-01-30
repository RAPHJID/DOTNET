using AutoMapper;
using PaymentsService.Models;
using PaymentsService.Models.Dtos;

namespace PaymentsService.Profiles
{
    public class PaymentsProfile: Profile
    {
        public PaymentsProfile()
        {
            
            CreateMap<Payment, MakePaymentsDto>().ReverseMap();

        }
    }
}
